// <copyright file="WebMeRouteConfiguration.cs" company="Stormpath, Inc.">
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
using System.Linq;

namespace Stormpath.Configuration.Abstractions.Immutable
{
    /// <summary>
    /// Represents configuration options for the Me route.
    /// </summary>
    public sealed class WebMeRouteConfiguration
    {
        public WebMeRouteConfiguration(
            IDictionary<string, bool> expand = null,
            bool? enabled = null,
            string uri = null)
        {
            Expand = new Dictionary<string, bool>(expand, StringComparer.OrdinalIgnoreCase);
            Enabled = enabled ?? false;
            Uri = uri;
        }

        internal WebMeRouteConfiguration()
        {
        }

        public WebMeRouteConfiguration DeepClone()
            => new WebMeRouteConfiguration(Expand.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), Enabled, Uri);

        /// <summary>
        /// The expansion options configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me.expand</c></remarks>
        public IReadOnlyDictionary<string, bool> Expand { get; internal set; }

        /// <summary>
        /// Determines whether the Me route is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}