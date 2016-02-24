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
using Stormpath.Configuration.Abstractions.Model;
using Stormpath.Configuration.Abstractions;
using Stormpath.SDK.Logging;
using FlexibleConfiguration;
using FlexibleConfiguration.Abstractions;

namespace Stormpath.Configuration
{
    /// <summary>
    /// Provides access to the local Stormpath configuration.
    /// </summary>
    public sealed class ConfigurationLoader
    {
        private static readonly string stormpathDirectoryPath = Path.Combine("~", ".stormpath");

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

            var output = new StormpathConfiguration(Default.Configuration);
            compiled.GetSection("stormpath").Bind(output);

            // Validate API Key and Secret exists
            ThrowIfMissingCredentials(output.Client);

            // Validation application href, if exists
            ThrowIfInvalidApplicationHref(output.Application);

            return output;
        }

        private IConfigurationRoot CompileFromSources()
        {
            var homeApiKeyPropertiesLocation = HomePath.Resolve("~", ".stormpath", "apiKey.properties");
            var homeStormpathJsonLocation = HomePath.Resolve("~", ".stormpath", "stormpath.json");
            var homeStormpathYamlLocation = HomePath.Resolve("~", ".stormpath", "stormpath.yaml");

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

            var apiKeyRootFile = apiKeyRootElement.GetValue<string>("file");
            if (!string.IsNullOrEmpty(apiKeyRootFile))
            {
                mappedProperties["stormpath:client:apiKey:file"] = apiKeyRootFile;
            }

            var apiKeyRootId = apiKeyRootElement.GetValue<string>("id");
            if (!string.IsNullOrEmpty(apiKeyRootId))
            {
                mappedProperties["stormpath:client:apiKey:id"] = apiKeyRootId;
            }

            var apiKeyRootSecret = apiKeyRootElement.GetValue<string>("secret");
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
            var specifiedApiKeyFilePath = builder.Build().GetValue("stormpath:client:apiKey:file", defaultValue: string.Empty);

            if (!string.IsNullOrEmpty(specifiedApiKeyFilePath))
            {
                logger.Trace($"Loading specified apiKey.properties file at {specifiedApiKeyFilePath}");

                builder.AddPropertiesFile(HomePath.Resolve(specifiedApiKeyFilePath), optional: false, root: "stormpath:client"); // Not optional this time!
            }
        }
    }
}
