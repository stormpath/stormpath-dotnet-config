﻿// <copyright file="ClientConfiguration.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Represents Client configuration options.
    /// </summary>
    public sealed class ClientConfiguration
    {
        /// <summary>
        /// The API Key configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.apiKey</c></remarks>
        public ClientApiKeyConfiguration ApiKey { get; set; }

        /// <summary>
        /// The cache configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager</c></remarks>
        public ClientCacheManagerConfiguration CacheManager { get; set; }

        /// <summary>
        /// The API base URL.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.baseUrl</c></remarks>
        public string BaseUrl { get; set; }

        /// <summary>
        /// The connection timeout.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.connectionTimeout</c></remarks>
        public int? ConnectionTimeout { get; set; }

        /// <summary>
        /// The authentication scheme to use for requests.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.authenticationScheme</c></remarks>
        public ClientAuthenticationScheme? AuthenticationScheme { get; set; }

        /// <summary>
        /// The proxy to use for requests.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.proxy</c></remarks>
        public ClientProxyConfiguration Proxy { get; set; }
    }
}
