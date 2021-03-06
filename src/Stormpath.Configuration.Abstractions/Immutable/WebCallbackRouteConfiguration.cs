﻿// <copyright file="WebCallbackRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the Stormpath callback route.
    /// </summary>
    /// <remarks>The Stormpath callback is used for ID Site, and will be used for other flows in the future.</remarks>
    public sealed class WebCallbackRouteConfiguration
    {
        public WebCallbackRouteConfiguration(
            bool? enabled = null,
            string uri = null)
        {
            Enabled = enabled ?? false;
            Uri = uri;
        }

        internal WebCallbackRouteConfiguration()
        {
        }

        public WebCallbackRouteConfiguration DeepClone()
            => new WebCallbackRouteConfiguration(Enabled, Uri);

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.callback.uri</c></remarks>
        public string Uri { get; set; }

        /// <summary>
        /// Determines whether the Stormpath callback handler route is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.callback.enabled</c></remarks>
        public bool Enabled { get; set; }
    }
}
