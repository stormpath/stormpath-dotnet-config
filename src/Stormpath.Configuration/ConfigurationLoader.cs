// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
// Copyright (c) 2016 Stormpath, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Contrib.Stormpath.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection;
using Microsoft.Extensions.Configuration.Contrib.Stormpath.PropertiesFile;
using Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml;
using Stormpath.Configuration.CustomProviders;
using Stormpath.Configuration.Abstractions.Model;
using Stormpath.Configuration.Abstractions;
using Stormpath.SDK.Logging;

#if !NET451
using Microsoft.Extensions.PlatformAbstractions;
#endif

namespace Stormpath.Configuration
{
    /// <summary>
    /// Provides access to the local Stormpath configuration.
    /// </summary>
    public sealed class ConfigurationLoader
    {
        private static readonly string stormpathDirectory = $"~{Path.DirectorySeparatorChar}.stormpath{Path.DirectorySeparatorChar}";

        private readonly object userConfiguration;
        private readonly ILogger logger;

        /// <summary>
        /// Loads the local Stormpath configuration and applies any user-defined configuration options.
        /// </summary>
        /// <param name="userConfiguration">
        /// An instance of <see cref="StormpathConfiguration"/>, or an anonymous type, containing user-defined configuration options.
        /// </param>
        /// <param name="logger">An optional logger.</param>
        /// <returns>The local Stormpath configuration.</returns>
        public static StormpathConfiguration Load(object userConfiguration = null, ILogger logger = null)
        {
            return new ConfigurationLoader(userConfiguration, logger)
                .Load();
        }

        private ConfigurationLoader(object userConfiguration, ILogger logger)
        {
            this.userConfiguration = userConfiguration;
            this.logger = logger;
        }

        private StormpathConfiguration Load()
        {
            var compiled = CompileFromSources();

            var output = new StormpathConfiguration();
            BindClientSection(compiled, output.Client);
            BindApplicationSection(compiled, output.Application);
            BindWebSection(compiled, output.Web);

            // Validate API Key and Secret exists
            ThrowIfMissingCredentials(output.Client);

            // Validation application href, if exists
            ThrowIfInvalidApplicationHref(output.Application);

            return output;
        }

        private IConfigurationRoot CompileFromSources()
        {
            var homeApiKeyPropertiesLocation = ResolveHomePath($"{stormpathDirectory}apiKey.properties");
            var homeStormpathJsonLocation = ResolveHomePath($"{stormpathDirectory}stormpath.json");
            var homeStormpathYamlLocation = ResolveHomePath($"{stormpathDirectory}stormpath.yaml");

            var configurationSources = string.Join(", ",
                homeApiKeyPropertiesLocation, homeStormpathJsonLocation, homeStormpathYamlLocation,
                "appsettings.json", "apiKey.properties", "stormpath.json", "stormpath.yaml",
                "Environment variables starting with 'STORMPATH_",
                this.userConfiguration == null ? null : "User-supplied configuration");
            logger.Trace($"Compiling configuration from sources: {configurationSources}");

            var builder = new ConfigurationBuilder()
                .AddPropertiesFile(homeApiKeyPropertiesLocation, optional: true, root: "stormpath:client")
                .AddJsonFile(homeStormpathJsonLocation, optional: true, root: "stormpath")
                .AddYamlFile(homeStormpathYamlLocation, optional: true, root: "stormpath")
                .AddJsonFile("appsettings.json", optional: true)
                .AddPropertiesFile("apiKey.properties", optional: true, root: "stormpath:client")
                .AddJsonFile("stormpath.json", optional: true, root: "stormpath")
                .AddYamlFile("stormpath.yaml", optional: true, root: "stormpath")
                .AddEnvironmentVariables("stormpath", "_", root: "stormpath")
                .AddObject(this.userConfiguration, root: "stormpath");

            // If a root key 'apiKey' is set, map to client.apiKey (for backwards compatibility)
            MapOrphanApiKeySection(builder);

            // If client.apiKey.file is set, load that
            LoadCustomApiKeyFileIfSpecified(builder);

            return builder.Build();
        }

        private void BindClientSection(IConfigurationRoot compiled, ClientConfiguration client)
        {
            logger.Trace("Binding section stormpath.client");
            client.BaseUrl = compiled.Get("stormpath:client:baseUrl", Default.Configuration.Client.BaseUrl);
            client.ConnectionTimeout = compiled.GetNullableInt("stormpath:client:connectionTimeout", Default.Configuration.Client.ConnectionTimeout);
            client.AuthenticationScheme = compiled.Get("stormpath:client:authenticationScheme", Default.Configuration.Client.AuthenticationScheme);

            logger.Trace("Binding section stormpath.client.apiKey");
            client.ApiKey.File = compiled.Get("stormpath:client:apiKey:file", Default.Configuration.Client.ApiKey.File);
            client.ApiKey.Id = compiled.Get("stormpath:client:apiKey:id", Default.Configuration.Client.ApiKey.Id);
            client.ApiKey.Secret = compiled.Get("stormpath:client:apiKey:secret", Default.Configuration.Client.ApiKey.Secret);

            logger.Trace("Binding section stormpath.client.cacheManager");
            client.CacheManager.DefaultTtl = compiled.GetNullableInt("stormpath:client:cacheManager:defaultTtl", Default.Configuration.Client.CacheManager.DefaultTtl);
            client.CacheManager.DefaultTti = compiled.GetNullableInt("stormpath:client:cacheManager:defaultTti", Default.Configuration.Client.CacheManager.DefaultTti);
            client.CacheManager.Caches = compiled.Get("stormpath:client:cacheManager:caches", new Dictionary<string, ClientCacheConfiguration>(Default.Configuration.Client.CacheManager.Caches));

            logger.Trace("Binding section stormpath.client.proxy");
            client.Proxy.Port = compiled.GetNullableInt("stormpath:client:proxy:port", Default.Configuration.Client.Proxy.Port);
            client.Proxy.Host = compiled.Get("stormpath:client:proxy:host", Default.Configuration.Client.Proxy.Host);
            client.Proxy.Username = compiled.Get("stormpath:client:proxy:username", Default.Configuration.Client.Proxy.Username);
            client.Proxy.Password = compiled.Get("stormpath:client:proxy:password", Default.Configuration.Client.Proxy.Password);
        }

        private void BindApplicationSection(IConfigurationRoot compiled, ApplicationConfiguration app)
        {
            logger.Trace("Binding section stormpath.application");
            app.Name = compiled.Get("stormpath:application:name", Default.Configuration.Application.Name);
            app.Href = compiled.Get("stormpath:application:href", Default.Configuration.Application.Href);
        }

        private void BindWebSection(IConfigurationRoot compiled, WebConfiguration web)
        {
            logger.Trace("Binding section stormpath.web");
            web.BasePath = compiled.Get("stormpath:web:basePath", Default.Configuration.Web.BasePath);

            logger.Trace("Binding section stormpath.web.oauth2");
            web.Oauth2.Enabled = compiled.GetNullableBool("stormpath:web:oauth2:enabled", Default.Configuration.Web.Oauth2.Enabled);
            web.Oauth2.Uri = compiled.Get("stormpath:web:oauth2:uri", Default.Configuration.Web.Oauth2.Uri);
            web.Oauth2.Client_Credentials.Enabled = compiled.GetNullableBool("stormpath:web:oauth2:client_credentials:enabled", Default.Configuration.Web.Oauth2.Client_Credentials.Enabled);
            web.Oauth2.Client_Credentials.AccessToken.Ttl = compiled.GetNullableInt("stormpath:web:oauth2:client_credentials:accessToken:ttl", Default.Configuration.Web.Oauth2.Client_Credentials.AccessToken.Ttl);
            web.Oauth2.Password.Enabled = compiled.GetNullableBool("stormpath:web:oauth2:password:enabled", Default.Configuration.Web.Oauth2.Password.Enabled);
            web.Oauth2.Password.ValidationStrategy = compiled.Get("stormpath:web:oauth2:password:validationStrategy", Default.Configuration.Web.Oauth2.Password.ValidationStrategy);

            logger.Trace("Binding section stormpath.web.expand");
            web.Expand = compiled.Get("stormpath:web:expand", Default.Configuration.Web.Expand);

            logger.Trace("Binding section stormpath.web.accessTokenCookie");
            web.AccessTokenCookie.Name = compiled.Get("stormpath:web:accessTokenCookie:name", Default.Configuration.Web.AccessTokenCookie.Name);
            web.AccessTokenCookie.HttpOnly = compiled.GetNullableBool("stormpath:web:accessTokenCookie:httpOnly", Default.Configuration.Web.AccessTokenCookie.HttpOnly);
            web.AccessTokenCookie.Secure = compiled.GetNullableBool("stormpath:web:accessTokenCookie:secure", Default.Configuration.Web.AccessTokenCookie.Secure);
            web.AccessTokenCookie.Path = compiled.Get("stormpath:web:accessTokenCookie:path", Default.Configuration.Web.AccessTokenCookie.Path);
            web.AccessTokenCookie.Domain = compiled.Get("stormpath:web:accessTokenCookie:domain", Default.Configuration.Web.AccessTokenCookie.Domain);

            logger.Trace("Binding section stormpath.web.refreshTokenCookie");
            web.RefreshTokenCookie.Name = compiled.Get("stormpath:web:refreshTokenCookie:name", Default.Configuration.Web.RefreshTokenCookie.Name);
            web.RefreshTokenCookie.HttpOnly = compiled.GetNullableBool("stormpath:web:refreshTokenCookie:httpOnly", Default.Configuration.Web.RefreshTokenCookie.HttpOnly);
            web.RefreshTokenCookie.Secure = compiled.GetNullableBool("stormpath:web:refreshTokenCookie:secure", Default.Configuration.Web.RefreshTokenCookie.Secure);
            web.RefreshTokenCookie.Path = compiled.Get("stormpath:web:refreshTokenCookie:path", Default.Configuration.Web.RefreshTokenCookie.Path);
            web.RefreshTokenCookie.Domain = compiled.Get("stormpath:web:refreshTokenCookie:domain", Default.Configuration.Web.RefreshTokenCookie.Domain);

            logger.Trace("Binding section stormpath.web.produces");
            web.Produces = compiled.Get("stormpath:web:produces", Default.Configuration.Web.Produces);

            logger.Trace("Binding section stormpath.web.register");
            web.Register.Enabled = compiled.GetNullableBool("stormpath:web:register:enabled", Default.Configuration.Web.Register.Enabled);
            web.Register.Uri = compiled.Get("stormpath:web:register:uri", Default.Configuration.Web.Register.Uri);
            web.Register.NextUri = compiled.Get("stormpath:web:register:nextUri", Default.Configuration.Web.Register.NextUri);
            web.Register.View = compiled.Get("stormpath:web:register:view", Default.Configuration.Web.Register.View);
            web.Register.AutoLogin = compiled.Get("stormpath:web:register:autoLogin", Default.Configuration.Web.Register.AutoLogin);
            web.Register.Form.Fields = compiled.Get("stormpath:web:register:form:fields", Default.Configuration.Web.Register.Form.Fields);
            web.Register.Form.FieldOrder = compiled.Get("stormpath:web:register:form:fieldOrder", Default.Configuration.Web.Register.Form.FieldOrder);

            logger.Trace("Binding section stormpath.web.verifyEmail");
            web.VerifyEmail.Enabled = compiled.GetNullableBool("stormpath:web:verifyEmail:enabled", Default.Configuration.Web.VerifyEmail.Enabled);
            web.VerifyEmail.Uri = compiled.Get("stormpath:web:verifyEmail:uri", Default.Configuration.Web.VerifyEmail.Uri);
            web.VerifyEmail.NextUri = compiled.Get("stormpath:web:verifyEmail:nextUri", Default.Configuration.Web.VerifyEmail.NextUri);
            web.VerifyEmail.View = compiled.Get("stormpath:web:verifyEmail:view", Default.Configuration.Web.VerifyEmail.View);

            logger.Trace("Binding section stormpath.web.login");
            web.Login.Enabled = compiled.GetNullableBool("stormpath:web:login:enabled", Default.Configuration.Web.Login.Enabled);
            web.Login.Uri = compiled.Get("stormpath:web:login:uri", Default.Configuration.Web.Login.Uri);
            web.Login.NextUri = compiled.Get("stormpath:web:login:nextUri", Default.Configuration.Web.Login.NextUri);
            web.Login.View = compiled.Get("stormpath:web:login:view", Default.Configuration.Web.Login.View);
            web.Login.Form.Fields = compiled.Get("stormpath:web:login:form:fields", Default.Configuration.Web.Login.Form.Fields);
            web.Login.Form.FieldOrder = compiled.Get("stormpath:web:login:form:fieldOrder", Default.Configuration.Web.Login.Form.FieldOrder);

            logger.Trace("Binding section stormpath.web.logout");
            web.Logout.Enabled = compiled.GetNullableBool("stormpath:web:logout:enabled", Default.Configuration.Web.Logout.Enabled);
            web.Logout.Uri = compiled.Get("stormpath:web:logout:uri", Default.Configuration.Web.Logout.Uri);
            web.Logout.NextUri = compiled.Get("stormpath:web:logout:nextUri", Default.Configuration.Web.Logout.NextUri);

            logger.Trace("Binding section stormpath.web.forgotPassword");
            web.ForgotPassword.Enabled = compiled.GetNullableBool("stormpath:web:forgotPassword:enabled", Default.Configuration.Web.ForgotPassword.Enabled);
            web.ForgotPassword.Uri = compiled.Get("stormpath:web:forgotPassword:uri", Default.Configuration.Web.ForgotPassword.Uri);
            web.ForgotPassword.NextUri = compiled.Get("stormpath:web:forgotPassword:nextUri", Default.Configuration.Web.ForgotPassword.NextUri);
            web.ForgotPassword.View = compiled.Get("stormpath:web:forgotPassword:view", Default.Configuration.Web.ForgotPassword.View);

            logger.Trace("Binding section stormpath.web.changePassword");
            web.ChangePassword.Enabled = compiled.GetNullableBool("stormpath:web:changePassword:enabled", Default.Configuration.Web.ChangePassword.Enabled);
            web.ChangePassword.Uri = compiled.Get("stormpath:web:changePassword:uri", Default.Configuration.Web.ChangePassword.Uri);
            web.ChangePassword.NextUri = compiled.Get("stormpath:web:changePassword:nextUri", Default.Configuration.Web.ChangePassword.NextUri);
            web.ChangePassword.View = compiled.Get("stormpath:web:changePassword:view", Default.Configuration.Web.ChangePassword.View);
            web.ChangePassword.AutoLogin = compiled.Get("stormpath:web:changePassword:autoLogin", Default.Configuration.Web.ChangePassword.AutoLogin);
            web.ChangePassword.ErrorUri = compiled.Get("stormpath:web:changePassword:errorUri", Default.Configuration.Web.ChangePassword.ErrorUri);

            logger.Trace("Binding section stormpath.web.idSite");
            web.IdSite.Enabled = compiled.GetNullableBool("stormpath:web:idSite:enabled", Default.Configuration.Web.IdSite.Enabled);
            web.IdSite.Uri = compiled.Get("stormpath:web:idSite:uri", Default.Configuration.Web.IdSite.Uri);
            web.IdSite.NextUri = compiled.Get("stormpath:web:idSite:nextUri", Default.Configuration.Web.IdSite.NextUri);
            web.IdSite.LoginUri = compiled.Get("stormpath:web:idSite:loginUri", Default.Configuration.Web.IdSite.LoginUri);
            web.IdSite.ForgotUri = compiled.Get("stormpath:web:idSite:forgotUri", Default.Configuration.Web.IdSite.ForgotUri);
            web.IdSite.RegisterUri = compiled.Get("stormpath:web:idSite:registerUri", Default.Configuration.Web.IdSite.RegisterUri);

            logger.Trace("Binding section stormpath.web.socialProviders");
            web.SocialProviders.CallbackRoot = compiled.Get("stormpath:web:socialProviders:callbackRoot", Default.Configuration.Web.SocialProviders.CallbackRoot);

            logger.Trace("Binding section stormpath.web.me");
            web.Me.Enabled = compiled.GetNullableBool("stormpath:web:me:enabled", Default.Configuration.Web.Me.Enabled);
            web.Me.Uri = compiled.Get("stormpath:web:me:uri", Default.Configuration.Web.Me.Uri);

            logger.Trace("Binding section stormpath.web.spa");
            web.Spa.Enabled = compiled.GetNullableBool("stormpath:web:spa:enabled", Default.Configuration.Web.Spa.Enabled);
            web.Spa.View = compiled.Get("stormpath:web:spa:view", Default.Configuration.Web.Spa.View);

            logger.Trace("Binding section stormpath.web.unauthorized");
            web.Unauthorized.View = compiled.Get("stormpath:web:unauthorized:view", Default.Configuration.Web.Unauthorized.View);
        }

        private void ThrowIfMissingCredentials(ClientConfiguration client)
        {
            logger.Trace("Validating API credentials");

            if (client?.ApiKey == null)
            {
                throw new ConfigurationException("API key cannot be empty.");
            }

            if (string.IsNullOrEmpty(client.ApiKey.Id)
                || string.IsNullOrEmpty(client.ApiKey.Secret))
            {
                throw new ConfigurationException("API key ID and secret is required.");
            }
        }

        private void ThrowIfInvalidApplicationHref(ApplicationConfiguration app)
        {
            if (string.IsNullOrEmpty(app?.Href))
            {
                return; // Skip validation
            }

            bool contains = app.Href.IndexOf("/applications/", StringComparison.OrdinalIgnoreCase) >= 0;
            if (!contains)
            {
                throw new ConfigurationException($"'{app.Href}' is not a valid Stormpath Application href.");
            }
        }

        private void MapOrphanApiKeySection(IConfigurationBuilder builder)
        {
            var apiKeyRootElement = builder.Build().GetSection("stormpath:apiKey");
            var mappedProperties = new Dictionary<string, string>();

            var apiKeyRootFile = apiKeyRootElement.Get<string>("file");
            if (!string.IsNullOrEmpty(apiKeyRootFile))
            {
                mappedProperties["stormpath:client:apiKey:file"] = apiKeyRootFile;
            }

            var apiKeyRootId = apiKeyRootElement.Get<string>("id");
            if (!string.IsNullOrEmpty(apiKeyRootId))
            {
                mappedProperties["stormpath:client:apiKey:id"] = apiKeyRootId;
            }

            var apiKeyRootSecret = apiKeyRootElement.Get<string>("secret");
            if (!string.IsNullOrEmpty(apiKeyRootSecret))
            {
                mappedProperties["stormpath:client:apiKey:secret"] = apiKeyRootSecret;
            }

            if (mappedProperties.Any())
            {
                logger.Warn("Mapping root-level apiKey properties to stormpath.client.apiKey for backwards compatibility. Switch to fully-qualified configuration with stormpath.client.apiKey.");

                builder.AddInMemoryCollection(mappedProperties);
            }
        }

        private void LoadCustomApiKeyFileIfSpecified(IConfigurationBuilder builder)
        {
            var specifiedApiKeyFilePath = builder.Build().Get("stormpath:client:apiKey:file", defaultValue: string.Empty);

            if (!string.IsNullOrEmpty(specifiedApiKeyFilePath))
            {
                logger.Trace($"Loading specified apiKey.properties file at {specifiedApiKeyFilePath}");

                builder.AddPropertiesFile(specifiedApiKeyFilePath, optional: false, root: "stormpath:client"); // Not optional this time!
            }
        }

        private string ResolveHomePath(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (!input.StartsWith("~"))
            {
                return input;
            }

            var homePath = GetHome();

            return System.IO.Path.Combine(homePath, input.Replace($"~{System.IO.Path.DirectorySeparatorChar}", string.Empty));
        }

        // Copied from DNX's DnuEnvironment.cs
        private static string GetHome()
        {
#if NET451
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#else
            var runtimeEnv = PlatformServices.Default.Runtime;
            if (runtimeEnv.OperatingSystem == "Windows")
            {
                return Environment.GetEnvironmentVariable("USERPROFILE") ??
                    Environment.GetEnvironmentVariable("HOMEDRIVE") + Environment.GetEnvironmentVariable("HOMEPATH");
            }
            else
            {
                var home = Environment.GetEnvironmentVariable("HOME");

                if (string.IsNullOrEmpty(home))
                {
                    throw new Exception("Home directory not found. The HOME environment variable is not set.");
                }

                return home;
            }
#endif
        }
    }
}
