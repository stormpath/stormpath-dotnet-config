// <copyright file="ClientCacheManagerConfiguration.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Abstractions.Immutable
{
    /// <summary>
    /// Represents high-level Client cache configuration options.
    /// </summary>
    public sealed class ClientCacheManagerConfiguration
    {
        public ClientCacheManagerConfiguration(
            bool? enabled = null,
            int? defaultTimeToLive = null,
            int? defaultTimeToIdle = null,
            IDictionary<string, ClientCacheConfiguration> caches = null)
        {
            this.Enabled = enabled ?? Default.Configuration.Client.CacheManager.Enabled;
            this.DefaultTtl = defaultTimeToLive ?? Default.Configuration.Client.CacheManager.DefaultTtl;
            this.DefaultTti = defaultTimeToIdle ?? Default.Configuration.Client.CacheManager.DefaultTti;
            this.Caches = new Dictionary<string, ClientCacheConfiguration>(caches ?? Default.Configuration.Client.CacheManager.Caches.ToDictionary());
        }

        public ClientCacheManagerConfiguration(ClientCacheManagerConfiguration existing)
            : this(enabled: existing?.Enabled,
                  defaultTimeToLive: existing?.DefaultTtl,
                  defaultTimeToIdle: existing?.DefaultTti,
                  caches: existing?.Caches?.ToDictionary())
        {
        }

        internal ClientCacheManagerConfiguration()
        {
        }

        /// <summary>
        /// Whether caching is enabled in the SDK client.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The default cache Time-To-Live, in seconds.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.defaultTtl</c></remarks>
        public int DefaultTtl { get; internal set; }

        /// <summary>
        /// The default cache Time-To-Idle, in seconds.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.defaultTti</c></remarks>
        public int DefaultTti { get; internal set; }

        /// <summary>
        /// Per-resource cache configurations.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.caches</c></remarks>
        public IReadOnlyDictionary<string, ClientCacheConfiguration> Caches { get; internal set; }
    }
}
