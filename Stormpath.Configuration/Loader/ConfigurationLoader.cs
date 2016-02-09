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
using Microsoft.Extensions.PlatformAbstractions;
using Stormpath.Configuration.Model;

namespace Stormpath.Configuration.Loader
{
    internal sealed class ConfigurationLoader
    {
        private static readonly string stormpathDirectory = $"~{Path.DirectorySeparatorChar}.stormpath{Path.DirectorySeparatorChar}";

        private readonly object userConfiguration;

        public ConfigurationLoader(object userConfiguration)
        {
            this.userConfiguration = userConfiguration;
        }

        public StormpathConfiguration Load()
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

            // Web discovery

            return output;
        }

        private IConfigurationRoot CompileFromSources()
        {
            var builder = new ConfigurationBuilder()
                .AddPropertiesFile(ResolveHomePath($"{stormpathDirectory}apiKey.properties"), optional: true, root: "client")
                .AddJsonFile(ResolveHomePath($"{stormpathDirectory}stormpath.json"), optional: true)
                .AddYamlFile($"{stormpathDirectory}stormpath.yaml", optional: true)
                .AddPropertiesFile("apiKey.properties", optional: true, root: "client")
                .AddJsonFile("stormpath.json", optional: true)
                .AddYamlFile("stormpath.yaml", optional: true)
                .AddEnvironmentVariables("stormpath", "_")
                .AddObject(this.userConfiguration);

            // If a root key 'apiKey' is set, map to client.apiKey (for backwards compatibility)
            var apiKeyRootElement = builder.Build().GetSection("apiKey");
            var mappedProperties = new Dictionary<string, string>();

            var apiKeyRootFile = apiKeyRootElement.Get<string>("file");
            if (!string.IsNullOrEmpty(apiKeyRootFile))
            {
                mappedProperties["client:apiKey:file"] = apiKeyRootFile;
            }

            var apiKeyRootId = apiKeyRootElement.Get<string>("id");
            if (!string.IsNullOrEmpty(apiKeyRootId))
            {
                mappedProperties["client:apiKey:id"] = apiKeyRootId;
            }

            var apiKeyRootSecret = apiKeyRootElement.Get<string>("secret");
            if (!string.IsNullOrEmpty(apiKeyRootSecret))
            {
                mappedProperties["client:apiKey:secret"] = apiKeyRootSecret;
            }

            if (mappedProperties.Any())
            {
                builder.AddInMemoryCollection(mappedProperties);
            }

            // If client.apiKey.file is set, load that
            var specifiedApiKeyFilePath = builder.Build().Get("client:apiKey:file", defaultValue: string.Empty);
            if (!string.IsNullOrEmpty(specifiedApiKeyFilePath))
            {
                builder.AddPropertiesFile(specifiedApiKeyFilePath, optional: false, root: "client"); // Not optional this time!
            }

            return builder.Build();
        }

        private void BindClientSection(IConfigurationRoot compiled, ClientConfiguration client)
        {
            client.ApiKey.File = compiled.Get("client:apiKey:file", Default.Configuration.Client.ApiKey.File);
            client.ApiKey.Id = compiled.Get("client:apiKey:id", Default.Configuration.Client.ApiKey.Id);
            client.ApiKey.Secret = compiled.Get("client:apiKey:secret", Default.Configuration.Client.ApiKey.Secret);

            client.CacheManager.DefaultTtl = compiled.GetNullableInt("client:cacheManager:defaultTtl", Default.Configuration.Client.CacheManager.DefaultTtl);
            client.CacheManager.DefaultTti = compiled.GetNullableInt("client:cacheManager:defaultTti", Default.Configuration.Client.CacheManager.DefaultTti);
            client.CacheManager.Caches = compiled.Get("client:cacheManager:caches", new Dictionary<string, ClientCacheConfiguration>(Default.Configuration.Client.CacheManager.Caches));

            client.BaseUrl = compiled.Get("client:baseUrl", Default.Configuration.Client.BaseUrl);
            client.ConnectionTimeout = compiled.GetNullableInt("client:connectionTimeout", Default.Configuration.Client.ConnectionTimeout);
            client.AuthenticationScheme = compiled.Get("client:authenticationScheme", Default.Configuration.Client.AuthenticationScheme);

            client.Proxy.Port = compiled.GetNullableInt("client:proxy:port", Default.Configuration.Client.Proxy.Port);
            client.Proxy.Host = compiled.Get("client:proxy:host", Default.Configuration.Client.Proxy.Host);
            client.Proxy.Username = compiled.Get("client:proxy:username", Default.Configuration.Client.Proxy.Username);
            client.Proxy.Password = compiled.Get("client:proxy:password", Default.Configuration.Client.Proxy.Password);
        }

        private void BindApplicationSection(IConfigurationRoot compiled, ApplicationConfiguration app)
        {
            app.Name = compiled.Get("application:name", Default.Configuration.Application.Name);
            app.Href = compiled.Get("application:href", Default.Configuration.Application.Href);
        }

        private void BindWebSection(IConfigurationRoot compiled, WebConfiguration web)
        {
            web.BasePath = compiled.Get("web:basePath", Default.Configuration.Web.BasePath);

            web.Oauth2.Enabled = compiled.GetNullableBool("web:oauth2:enabled", Default.Configuration.Web.Oauth2.Enabled);
            web.Oauth2.Uri = compiled.Get("web:oauth2:uri", Default.Configuration.Web.Oauth2.Uri);
            web.Oauth2.Client_Credentials.Enabled = compiled.GetNullableBool("web:oauth2:client_credentials:enabled", Default.Configuration.Web.Oauth2.Client_Credentials.Enabled);
            web.Oauth2.Client_Credentials.AccessToken.Ttl = compiled.GetNullableInt("web:oauth2:client_credentials:accessToken:ttl", Default.Configuration.Web.Oauth2.Client_Credentials.AccessToken.Ttl);
            web.Oauth2.Password.Enabled = compiled.GetNullableBool("web:oauth2:password:enabled", Default.Configuration.Web.Oauth2.Password.Enabled);
            web.Oauth2.Password.ValidationStrategy = compiled.Get("web:oauth2:password:validationStrategy", Default.Configuration.Web.Oauth2.Password.ValidationStrategy);

            web.Expand = compiled.Get("web:expand", Default.Configuration.Web.Expand);

            web.AccessTokenCookie.Name = compiled.Get("web:accessTokenCookie:name", Default.Configuration.Web.AccessTokenCookie.Name);
            web.AccessTokenCookie.HttpOnly = compiled.GetNullableBool("web:accessTokenCookie:httpOnly", Default.Configuration.Web.AccessTokenCookie.HttpOnly);
            web.AccessTokenCookie.Secure = compiled.GetNullableBool("web:accessTokenCookie:secure", Default.Configuration.Web.AccessTokenCookie.Secure);
            web.AccessTokenCookie.Path = compiled.Get("web:accessTokenCookie:path", Default.Configuration.Web.AccessTokenCookie.Path);
            web.AccessTokenCookie.Domain = compiled.Get("web:accessTokenCookie:domain", Default.Configuration.Web.AccessTokenCookie.Domain);

            web.RefreshTokenCookie.Name = compiled.Get("web:refreshTokenCookie:name", Default.Configuration.Web.RefreshTokenCookie.Name);
            web.RefreshTokenCookie.HttpOnly = compiled.GetNullableBool("web:refreshTokenCookie:httpOnly", Default.Configuration.Web.RefreshTokenCookie.HttpOnly);
            web.RefreshTokenCookie.Secure = compiled.GetNullableBool("web:refreshTokenCookie:secure", Default.Configuration.Web.RefreshTokenCookie.Secure);
            web.RefreshTokenCookie.Path = compiled.Get("web:refreshTokenCookie:path", Default.Configuration.Web.RefreshTokenCookie.Path);
            web.RefreshTokenCookie.Domain = compiled.Get("web:refreshTokenCookie:domain", Default.Configuration.Web.RefreshTokenCookie.Domain);

            web.Produces = compiled.Get("web:produces", Default.Configuration.Web.Produces);

            web.Register.Enabled = compiled.GetNullableBool("web:register:enabled", Default.Configuration.Web.Register.Enabled);
            web.Register.Uri = compiled.Get("web:register:uri", Default.Configuration.Web.Register.Uri);
            web.Register.NextUri = compiled.Get("web:register:nextUri", Default.Configuration.Web.Register.NextUri);
            web.Register.View = compiled.Get("web:register:view", Default.Configuration.Web.Register.View);
            web.Register.AutoLogin = compiled.Get("web:register:autoLogin", Default.Configuration.Web.Register.AutoLogin);
            web.Register.Form.Fields = compiled.Get("web:register:form:fields", Default.Configuration.Web.Register.Form.Fields);
            web.Register.Form.FieldOrder = compiled.Get("web:register:form:fieldOrder", Default.Configuration.Web.Register.Form.FieldOrder);

            web.VerifyEmail.Enabled = compiled.GetNullableBool("web:verifyEmail:enabled", Default.Configuration.Web.VerifyEmail.Enabled);
            web.VerifyEmail.Uri = compiled.Get("web:verifyEmail:uri", Default.Configuration.Web.VerifyEmail.Uri);
            web.VerifyEmail.NextUri = compiled.Get("web:verifyEmail:nextUri", Default.Configuration.Web.VerifyEmail.NextUri);
            web.VerifyEmail.View = compiled.Get("web:verifyEmail:view", Default.Configuration.Web.VerifyEmail.View);

            web.Login.Enabled = compiled.GetNullableBool("web:login:enabled", Default.Configuration.Web.Login.Enabled);
            web.Login.Uri = compiled.Get("web:login:uri", Default.Configuration.Web.Login.Uri);
            web.Login.NextUri = compiled.Get("web:login:nextUri", Default.Configuration.Web.Login.NextUri);
            web.Login.View = compiled.Get("web:login:view", Default.Configuration.Web.Login.View);
            web.Login.Form.Fields = compiled.Get("web:login:form:fields", Default.Configuration.Web.Login.Form.Fields);
            web.Login.Form.FieldOrder = compiled.Get("web:login:form:fieldOrder", Default.Configuration.Web.Login.Form.FieldOrder);

            web.Logout.Enabled = compiled.GetNullableBool("web:logout:enabled", Default.Configuration.Web.Logout.Enabled);
            web.Logout.Uri = compiled.Get("web:logout:uri", Default.Configuration.Web.Logout.Uri);
            web.Logout.NextUri = compiled.Get("web:logout:nextUri", Default.Configuration.Web.Logout.NextUri);

            web.ForgotPassword.Enabled = compiled.GetNullableBool("web:forgotPassword:enabled", Default.Configuration.Web.ForgotPassword.Enabled);
            web.ForgotPassword.Uri = compiled.Get("web:forgotPassword:uri", Default.Configuration.Web.ForgotPassword.Uri);
            web.ForgotPassword.NextUri = compiled.Get("web:forgotPassword:nextUri", Default.Configuration.Web.ForgotPassword.NextUri);
            web.ForgotPassword.View = compiled.Get("web:forgotPassword:view", Default.Configuration.Web.ForgotPassword.View);

            web.ChangePassword.Enabled = compiled.GetNullableBool("web:changePassword:enabled", Default.Configuration.Web.ChangePassword.Enabled);
            web.ChangePassword.Uri = compiled.Get("web:changePassword:uri", Default.Configuration.Web.ChangePassword.Uri);
            web.ChangePassword.NextUri = compiled.Get("web:changePassword:nextUri", Default.Configuration.Web.ChangePassword.NextUri);
            web.ChangePassword.View = compiled.Get("web:changePassword:view", Default.Configuration.Web.ChangePassword.View);
            web.ChangePassword.AutoLogin = compiled.Get("web:changePassword:autoLogin", Default.Configuration.Web.ChangePassword.AutoLogin);
            web.ChangePassword.ErrorUri = compiled.Get("web:changePassword:errorUri", Default.Configuration.Web.ChangePassword.ErrorUri);

            web.IdSite.Enabled = compiled.GetNullableBool("web:idSite:enabled", Default.Configuration.Web.IdSite.Enabled);
            web.IdSite.Uri = compiled.Get("web:idSite:uri", Default.Configuration.Web.IdSite.Uri);
            web.IdSite.NextUri = compiled.Get("web:idSite:nextUri", Default.Configuration.Web.IdSite.NextUri);
            web.IdSite.LoginUri = compiled.Get("web:idSite:loginUri", Default.Configuration.Web.IdSite.LoginUri);
            web.IdSite.ForgotUri = compiled.Get("web:idSite:forgotUri", Default.Configuration.Web.IdSite.ForgotUri);
            web.IdSite.RegisterUri = compiled.Get("web:idSite:registerUri", Default.Configuration.Web.IdSite.RegisterUri);

            web.SocialProviders.CallbackRoot = compiled.Get("web:socialProviders:callbackRoot", Default.Configuration.Web.SocialProviders.CallbackRoot);

            web.Me.Enabled = compiled.GetNullableBool("web:me:enabled", Default.Configuration.Web.Me.Enabled);
            web.Me.Uri = compiled.Get("web:me:uri", Default.Configuration.Web.Me.Uri);

            web.Spa.Enabled = compiled.GetNullableBool("web:spa:enabled", Default.Configuration.Web.Spa.Enabled);
            web.Spa.View = compiled.Get("web:spa:view", Default.Configuration.Web.Spa.View);

            web.Unauthorized.View = compiled.Get("web:unauthorized:view", Default.Configuration.Web.Unauthorized.View);
        }

        private void ThrowIfMissingCredentials(ClientConfiguration client)
        {
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
#if DNX451
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
