// <copyright file="DefaultConfigurationLoader.cs" company="Stormpath, Inc.">
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
using FlexibleConfiguration;
using FlexibleConfiguration.Abstractions;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.Configuration
{
    internal sealed class DefaultConfigurationLoader : IConfigurationLoader
    {
        private static readonly string stormpathDirectoryPath = Path.Combine("~", ".stormpath");

        public Abstractions.Immutable.StormpathConfiguration Load(object userConfiguration = null)
        {
            var compiled = CompileFromSources(userConfiguration); // TODO restore logging

            var output = new Abstractions.Immutable.StormpathConfiguration(Default.Configuration);
            compiled.GetSection("stormpath").Bind(output);

            // Validate API Key and Secret exists
            ThrowIfMissingCredentials(output.Client); // TODO restore logging

            // Validation application href, if exists
            ThrowIfInvalidApplicationHref(output.Application);

            return output;
        }

        private static IConfigurationRoot CompileFromSources(object userConfiguration)
        {
            var homeApiKeyPropertiesLocation = HomePath.Resolve("~", ".stormpath", "apiKey.properties");
            var homeStormpathJsonLocation = HomePath.Resolve("~", ".stormpath", "stormpath.json");
            var homeStormpathYamlLocation = HomePath.Resolve("~", ".stormpath", "stormpath.yaml");

            var configurationSources = string.Join(", ",
                homeApiKeyPropertiesLocation, homeStormpathJsonLocation, homeStormpathYamlLocation,
                "appsettings.json", "apiKey.properties", "stormpath.json", "stormpath.yaml",
                "Environment variables starting with 'STORMPATH_",
                userConfiguration == null ? null : "User-supplied configuration");
            //logger.Trace($"Compiling configuration from sources: {configurationSources}");

            var builder = new ConfigurationBuilder()
                .AddPropertiesFile(homeApiKeyPropertiesLocation, optional: true, root: "stormpath:client")
                .AddJsonFile(homeStormpathJsonLocation, optional: true, root: "stormpath")
                .AddYamlFile(homeStormpathYamlLocation, optional: true, root: "stormpath")
                .AddJsonFile("appsettings.json", optional: true)
                .AddPropertiesFile("apiKey.properties", optional: true, root: "stormpath:client")
                .AddJsonFile("stormpath.json", optional: true, root: "stormpath")
                .AddYamlFile("stormpath.yaml", optional: true, root: "stormpath")
                .AddEnvironmentVariables("stormpath", "_", root: "stormpath")
                .AddObject(userConfiguration, root: "stormpath");

            // If a root key 'apiKey' is set, map to client.apiKey (for backwards compatibility)
            MapOrphanApiKeySection(builder); // TODO restore logging

            // If client.apiKey.file is set, load that
            LoadCustomApiKeyFileIfSpecified(builder); // TODO restore logging

            // Apply rules to cookie paths
            UpdateCookiePath(builder, "accessTokenCookie");
            UpdateCookiePath(builder, "refreshTokenCookie");

            return builder.Build();
        }

        private static void ThrowIfMissingCredentials(Abstractions.Immutable.ClientConfiguration client)
        {
            //logger.Trace("Validating API credentials");

            if (client?.ApiKey == null)
            {
                throw new ConfigurationException("API key cannot be empty.");
            }

            if (string.IsNullOrEmpty(client.ApiKey.Id))
            {
                throw new ConfigurationException("API key ID is required.");
            }

            if (string.IsNullOrEmpty(client.ApiKey.Secret))
            {
                throw new ConfigurationException("API key secret is required.");
            }
        }

        private static void ThrowIfInvalidApplicationHref(Abstractions.Immutable.ApplicationConfiguration app)
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

        private static void MapOrphanApiKeySection(IConfigurationBuilder builder)
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
                //logger.Warn("Mapping root-level apiKey properties to stormpath.client.apiKey for backwards compatibility. Switch to fully-qualified configuration with stormpath.client.apiKey.");

                builder.AddInMemoryCollection(mappedProperties);
            }
        }

        private static void LoadCustomApiKeyFileIfSpecified(IConfigurationBuilder builder)
        {
            var specifiedApiKeyFilePath = builder.Build().GetValue("stormpath:client:apiKey:file", defaultValue: string.Empty);

            if (!string.IsNullOrEmpty(specifiedApiKeyFilePath))
            {
                //logger.Trace($"Loading specified apiKey.properties file at {specifiedApiKeyFilePath}");

                builder.AddPropertiesFile(HomePath.Resolve(specifiedApiKeyFilePath), optional: false, root: "stormpath:client"); // Not optional this time!
            }
        }

        private static void UpdateCookiePath(IConfigurationBuilder builder, string cookieName)
        {
            var built = builder.Build();

            var basePath = built.GetValue<string>("stormpath:web:basePath");
            var cookiePath = built.GetValue<string>($"stormpath:web:{cookieName}:path");

            if (string.IsNullOrEmpty(cookiePath))
            {
                var defaultPath = string.IsNullOrEmpty(basePath)
                    ? "/"
                    : basePath;

                builder.AddInMemoryCollection(new Dictionary<string, string>()
                {
                    [$"stormpath:web:{cookieName}:path"] = defaultPath
                });
            }
        }
    }
}
