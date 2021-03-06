﻿// <copyright file="WebLoginRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the Login route.
    /// </summary>
    public sealed class WebLoginRouteConfiguration
    {
        /// <summary>
        /// The form configuration options.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.form</c></remarks>
        public WebLoginRouteFormConfiguration Form { get; set; }

        /// <summary>
        /// The view to use for this route, or <see langword="null"/> to use the default view.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.view</c></remarks>
        public string View { get; set; } = Default.Configuration.Web.Login.View;

        /// <summary>
        /// The URI to redirect to if the operation is successful.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.nextUri</c></remarks>
        public string NextUri { get; set; } = Default.Configuration.Web.Login.NextUri;

        /// <summary>
        /// Determines whether the Login route is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.enabled</c></remarks>
        public bool Enabled { get; set; } = Default.Configuration.Web.Login.Enabled;

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.uri</c></remarks>
        public string Uri { get; set; } = Default.Configuration.Web.Login.Uri;
    }
}
