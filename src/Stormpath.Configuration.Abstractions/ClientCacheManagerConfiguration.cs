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

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Represents high-level Client cache configuration options.
    /// </summary>
    public sealed class ClientCacheManagerConfiguration
    {
        /// <summary>
        /// The default cache Time-To-Live.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.defaultTtl</c></remarks>
        public int? DefaultTtl { get; set; }

        /// <summary>
        /// The default cache Time-To-Idle.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.defaultTti</c></remarks>
        public int? DefaultTti { get; set; }

        /// <summary>
        /// Per-resource cache configurations.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.cacheManager.caches</c></remarks>
        public IReadOnlyDictionary<string, ClientCacheConfiguration> Caches { get; set; }
    }
}
