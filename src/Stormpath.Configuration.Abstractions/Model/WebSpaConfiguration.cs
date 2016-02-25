// <copyright file="WebSpaConfiguration.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Abstractions.Model
{
    /// <summary>
    /// Represents configuration options for Single-Page Application support.
    /// </summary>
    /// <remarks>
    /// If the developer wants our integration to serve their Single Page
    /// Application (SPA) in response to HTML requests for our default routes,
    /// such as <c>/login</c>, then they will need to enable this feature and tell us
    /// where the root of their SPA is.  This is likely a file path on the file system.
    /// </remarks>
    public sealed class WebSpaConfiguration
    {
        public WebSpaConfiguration(
            bool? enabled = null,
            string view = null)
        {
            this.Enabled = enabled ?? Default.Configuration.Web.Spa.Enabled;
            this.View = view ?? Default.Configuration.Web.Spa.View;
        }

        public WebSpaConfiguration(WebSpaConfiguration existing)
            : this(enabled: existing.Enabled,
                  view: existing.View)
        {
        }

        internal WebSpaConfiguration()
        {
        }

        /// <summary>
        /// Determines whether the SPA handling options are enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.spa.enabled</c></remarks>
        public bool? Enabled { get; internal set; }

        /// <summary>
        /// The root view of the SPA.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.spa.view</c></remarks>
        public string View { get; internal set; }
    }
}