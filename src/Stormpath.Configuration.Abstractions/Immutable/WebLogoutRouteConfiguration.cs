﻿// <copyright file="WebLogoutRouteConfiguration.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Abstractions.Immutable
{
    /// <summary>
    /// Represents configuration options for the Change Password route.
    /// </summary>
    public sealed class WebLogoutRouteConfiguration
    {
        public WebLogoutRouteConfiguration(
            string nextUri = null,
            bool? enabled = null,
            string uri = null)
        {
            NextUri = nextUri;
            Enabled = enabled ?? false;
            Uri = uri;
        }

        internal WebLogoutRouteConfiguration()
        {
        }

        public WebLogoutRouteConfiguration DeepClone()
            => new WebLogoutRouteConfiguration(NextUri, Enabled, Uri);

        /// <summary>
        /// The URI to redirect to if the logout is successful.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.logout.nextUri</c></remarks>
        public string NextUri { get; internal set; }

        /// <summary>
        /// Determines whether the Logout route is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.logout.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.logout.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}