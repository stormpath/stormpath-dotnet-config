﻿// <copyright file="WebIdSiteRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the ID Site route.
    /// </summary>
    public sealed class WebIdSiteRouteConfiguration
    {
        public WebIdSiteRouteConfiguration(
            string loginUri = null,
            string forgotUri = null,
            string registerUri = null,
            string nextUri = null,
            bool? enabled = null,
            string uri = null)
        {
            this.LoginUri = loginUri ?? Default.Configuration.Web.IdSite.LoginUri;
            this.ForgotUri = forgotUri ?? Default.Configuration.Web.IdSite.ForgotUri;
            this.RegisterUri = registerUri ?? Default.Configuration.Web.IdSite.RegisterUri;
            this.NextUri = nextUri ?? Default.Configuration.Web.IdSite.NextUri;
            this.Enabled = enabled ?? Default.Configuration.Web.IdSite.Enabled;
            this.Uri = uri ?? Default.Configuration.Web.IdSite.Uri;
        }

        public WebIdSiteRouteConfiguration(WebIdSiteRouteConfiguration existing)
            : this(loginUri: existing?.LoginUri,
                  forgotUri: existing?.ForgotUri,
                  registerUri: existing?.RegisterUri,
                  nextUri: existing?.NextUri,
                  enabled: existing?.Enabled,
                  uri: existing?.Uri)
        {
        }

        internal WebIdSiteRouteConfiguration()
        {
        }

        /// <summary>
        /// The URI for the ID Site Login handler, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.loginUri</c></remarks>
        public string LoginUri { get; internal set; }

        /// <summary>
        /// The URI for the ID Site Forgot Password handler, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.forgotUri</c></remarks>
        public string ForgotUri { get; internal set; }

        /// <summary>
        /// The URI for the ID Site Register handler, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.registerUri</c></remarks>
        public string RegisterUri { get; internal set; }

        /// <summary>
        /// The URI to redirect to if the operation is successful.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.nextUri</c></remarks>
        public string NextUri { get; internal set; }

        /// <summary>
        /// Determines whether ID Site is used for login, registration, and password reset.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}