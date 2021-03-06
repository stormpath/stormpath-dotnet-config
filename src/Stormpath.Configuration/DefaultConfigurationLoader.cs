﻿// <copyright file="DefaultConfigurationLoader.cs" company="Stormpath, Inc.">
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

using System.Collections.Generic;
using System.IO;
using FlexibleConfiguration;
using FlexibleConfiguration.Abstractions;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.Configuration
{
    internal sealed class DefaultConfigurationLoader : IConfigurationLoader
    {
        public Abstractions.Immutable.StormpathConfiguration Load(object userConfiguration = null, string configurationFileRoot = null)
        {
            var compiled = CompileFromSources(userConfiguration, configurationFileRoot); // TODO restore logging

            var output = Default.Configuration.DeepClone();
            compiled.GetSection("okta").Bind(output);

            return output;
        }

        private static IConfigurationRoot CompileFromSources(object userConfiguration, string configurationFileRoot)
        {
            var homeStormpathJsonLocation = HomePath.Resolve("~", ".stormpath", "stormpath.json");
            var homeOktaJsonLocation = HomePath.Resolve("~", ".stormpath", "okta.json");
            var homeStormpathYamlLocation = HomePath.Resolve("~", ".stormpath", "stormpath.yaml");
            var homeOktaYamlLocation = HomePath.Resolve("~", ".stormpath", "okta.yaml");

            var applicationAppSettingsLocation = Path.Combine(configurationFileRoot ?? string.Empty, "appsettings.json");
            var applicationStormpathJsonLocation = Path.Combine(configurationFileRoot ?? string.Empty, "stormpath.json");
            var applicationOktaJsonLocation = Path.Combine(configurationFileRoot ?? string.Empty, "okta.json");
            var applicationStormpathYamlLocation = Path.Combine(configurationFileRoot ?? string.Empty, "stormpath.yaml");
            var applicationOktaYamlLocation = Path.Combine(configurationFileRoot ?? string.Empty, "okta.yaml");

            // TODO logging
            //var configurationSources = string.Join(", ",
            //    homeApiKeyPropertiesLocation,
            //    homeStormpathJsonLocation,
            //    homeStormpathYamlLocation,
            //    applicationAppSettingsLocation,
            //    applicationApiKeyPropertiesLocation,
            //    applicationStormpathJsonLocation,
            //    applicationStormpathYamlLocation,
            //    "Environment variables starting with 'STORMPATH_",
            //    userConfiguration == null ? null : "User-supplied configuration");
            //logger.Trace($"Compiling configuration from sources: {configurationSources}");

            var builder = new ConfigurationBuilder()
                .AddJsonFile(homeStormpathJsonLocation, optional: true, root: "okta")
                .AddYamlFile(homeStormpathYamlLocation, optional: true, root: "okta")
                .AddJsonFile(applicationAppSettingsLocation, optional: true)
                .AddJsonFile(applicationStormpathJsonLocation, optional: true, root: "okta")
                .AddYamlFile(applicationStormpathYamlLocation, optional: true, root: "okta")
                .AddEnvironmentVariables("stormpath", "_", root: "okta")
                .AddEnvironmentVariables("okta", "_", root: "okta")
                .AddObject(userConfiguration, root: "okta");

            // Apply rules to cookie paths
            UpdateCookiePath(builder, "accessTokenCookie");
            UpdateCookiePath(builder, "refreshTokenCookie");

            FixOrgUrlTrailingSlash(builder);

            return builder.Build();
        }

        private static void UpdateCookiePath(IConfigurationBuilder builder, string cookieName)
        {
            var built = builder.Build();

            var basePath = built.GetValue<string>("okta:web:basePath");
            var cookiePath = built.GetValue<string>($"okta:web:{cookieName}:path");

            if (string.IsNullOrEmpty(cookiePath))
            {
                var defaultPath = string.IsNullOrEmpty(basePath)
                    ? "/"
                    : basePath;

                builder.AddInMemoryCollection(new Dictionary<string, string>()
                {
                    [$"okta:web:{cookieName}:path"] = defaultPath
                });
            }
        }

        private static void FixOrgUrlTrailingSlash(IConfigurationBuilder builder)
        {
            var built = builder.Build();

            var orgUrl = built.GetValue<string>("okta:org");

            if (string.IsNullOrEmpty(orgUrl)) return;
            if (!orgUrl.EndsWith("/")) return;

            var sanitizedOrgUrl = orgUrl.TrimEnd('/');

            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                [$"okta:org"] = sanitizedOrgUrl
            });
        }
    }
}
