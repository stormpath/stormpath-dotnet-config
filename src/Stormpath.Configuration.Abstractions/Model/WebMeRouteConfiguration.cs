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

using System.Collections.Generic;

namespace Stormpath.Configuration.Abstractions.Model
{
    /// <summary>
    /// Represents configuration options for the Me route.
    /// </summary>
    public sealed class WebMeRouteConfiguration
    {
        public WebMeRouteConfiguration(
            IDictionary<string, bool> expand,
            bool? enabled,
            string uri)
        {
            this.Expand = new Dictionary<string, bool>(expand);
            this.Enabled = enabled;
            this.Uri = uri;
        }

        public WebMeRouteConfiguration(WebMeRouteConfiguration existing)
            : this(expand: existing.Expand,
                  enabled: existing.Enabled,
                  uri: existing.Uri)
        {
        }

        internal WebMeRouteConfiguration()
        {
        }

        /// <summary>
        /// The expansion options configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me.expand</c></remarks>
        public Dictionary<string, bool> Expand { get; internal set; }

        /// <summary>
        /// Determines whether the Me route is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me.enabled</c></remarks>
        public bool? Enabled { get; internal set; }

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}