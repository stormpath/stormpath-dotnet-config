﻿// <copyright file="WebForgotPasswordRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the Forgot Password route.
    /// </summary>
    public sealed class WebForgotPasswordRouteConfiguration
    {
        public WebForgotPasswordRouteConfiguration(
            string view = null,
            string nextUri = null,
            bool? enabled = null,
            string uri = null)
        {
            this.View = view ?? Default.Configuration.Web.ForgotPassword.View;
            this.NextUri = nextUri ?? Default.Configuration.Web.ForgotPassword.NextUri;
            this.Enabled = enabled ?? Default.Configuration.Web.ForgotPassword.Enabled;
            this.Uri = uri ?? Default.Configuration.Web.ForgotPassword.Uri;
        }

        public WebForgotPasswordRouteConfiguration(WebForgotPasswordRouteConfiguration existing)
            : this(view: existing?.View,
                  nextUri: existing?.NextUri,
                  enabled: existing?.Enabled,
                  uri: existing?.Uri)
        {
        }

        internal WebForgotPasswordRouteConfiguration()
        {
        }

        /// <summary>
        /// The view to use for this route, or <see langword="null"/> to use the default view.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.forgotPassword.view</c></remarks>
        public string View { get; internal set; }

        /// <summary>
        /// The URI to redirect to if the operation is successful.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.forgotPassword.nextUri</c></remarks>
        public string NextUri { get; internal set; }

        /// <summary>
        /// Determines whether the Forgot Password route is enabled.
        /// </summary>
        /// <remarks>
        /// Unless explicitly set to <see langword="false"/>, this feature
        /// will be automatically enabled if the default account store for the defined
        /// Stormpath application has the password reset workflow enabled.
        /// <para>
        /// Configuration path: <c>stormpath.web.forgotPassword.enabled</c>
        /// </para>
        /// </remarks>
        public bool? Enabled { get; internal set; }

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.forgotPassword.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}