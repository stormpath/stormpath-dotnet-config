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

namespace Stormpath.Configuration.Abstractions.Model
{
    /// <summary>
    /// Represents high-level Client cache configuration options.
    /// </summary>
    public sealed class ClientCacheManagerConfiguration
    {
        public ClientCacheManagerConfiguration(
            int? defaultTimeToLive,
            int? defaultTimeToIdle,
            IDictionary<string, ClientCacheConfiguration> caches)
        {
            this.DefaultTtl = defaultTimeToLive;
            this.DefaultTti = defaultTimeToIdle;
            this.Caches = new Dictionary<string, ClientCacheConfiguration>(caches);
        }

        public ClientCacheManagerConfiguration(ClientCacheManagerConfiguration existing)
            : this(defaultTimeToLive: existing.DefaultTtl,
                  defaultTimeToIdle: existing.DefaultTti,
                  caches: existing.Caches)
        {
        }

        internal ClientCacheManagerConfiguration()
        {
        }

        /// <summary>
        /// The default cache Time-To-Live.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.defaultTtl</c></remarks>
        public int? DefaultTtl { get; internal set; }

        /// <summary>
        /// The default cache Time-To-Idle.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.defaultTti</c></remarks>
        public int? DefaultTti { get; internal set; }

        /// <summary>
        /// Per-resource cache configurations.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.caches</c></remarks>
        public Dictionary<string, ClientCacheConfiguration> Caches { get; internal set; }
    }
}
