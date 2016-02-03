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

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Contrib.Stormpath.PropertiesFile;
using Stormpath.Configuration.Model;

namespace Stormpath.Configuration.Loader
{
    internal sealed class ConfigurationLoader
    {
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
            BindApplicationSection(compiled, output);
            BindWebSection(compiled, output);

            // Validate API Key and Secret

            // Web Validation?

            return output;
        }

        private IConfigurationRoot CompileFromSources()
        {
            var builder = new ConfigurationBuilder()
                .AddPropertiesFile("~/.stormpath/apiKey.properties", optional: true, prefix: "client")
                .AddJsonFile("~/.stormpath/stormpath.json", optional: true)
                //.AddYamlFile("~/.stormpath/stormpath.yaml", optional: true)
                .AddPropertiesFile("apiKey.properties", optional: true, prefix: "client")
                .AddJsonFile("stormpath.json", optional: true)
                //.AddYamlFile("stormpath.yaml", optional: true)
                //.AddMatchingEnvironmentVariables(environment, match: Default.Configuration)
                //.AddObject(this.userConfiguration)
                ;

            return builder.Build();
        }

        private void BindClientSection(IConfigurationRoot compiled, ClientConfiguration client)
        {
            client.ApiKey.File = compiled.Get("client:apiKey:file", Default.Configuration.Client.ApiKey.File);
            client.ApiKey.Id = compiled.Get("client:apiKey:id", Default.Configuration.Client.ApiKey.Id);
            client.ApiKey.Secret = compiled.Get("client:apiKey:secret", Default.Configuration.Client.ApiKey.Secret);

            client.CacheManager.DefaultTtl = compiled.Get("client:cacheManager:defaultTtl", Default.Configuration.Client.CacheManager.DefaultTtl);
            client.CacheManager.DefaultTti = compiled.Get("client:cacheManager:defaultTti", Default.Configuration.Client.CacheManager.DefaultTti);
            client.CacheManager.Caches = compiled.Get<Dictionary<string, ClientCacheConfiguration>>("client:cacheManager:caches");

            client.BaseUrl = compiled.Get("client:baseUrl", Default.Configuration.Client.BaseUrl);
            client.ConnectionTimeout = compiled.Get("client:connectionTimeout", Default.Configuration.Client.ConnectionTimeout);
            client.AuthenticationScheme = compiled.Get("client:authenticationScheme", Default.Configuration.Client.AuthenticationScheme);

            client.Proxy.Port = compiled.Get("client:proxy:port", Default.Configuration.Client.Proxy.Port);
            client.Proxy.Host = compiled.Get("client:proxy:host", Default.Configuration.Client.Proxy.Host);
            client.Proxy.Username = compiled.Get("client:proxy:username", Default.Configuration.Client.Proxy.Username);
            client.Proxy.Password = compiled.Get("client:proxy:password", Default.Configuration.Client.Proxy.Password);
        }

        private void BindApplicationSection(IConfigurationRoot compiled, StormpathConfiguration output)
        {
            output.Application.Name = compiled.Get("application:name", Default.Configuration.Application.Name);
            output.Application.Href = compiled.Get("application:href", Default.Configuration.Application.Href);
        }

        private void BindWebSection(IConfigurationRoot compiled, StormpathConfiguration output)
        {
            //todo
        }
    }
}
