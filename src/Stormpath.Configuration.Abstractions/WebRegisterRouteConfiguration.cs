﻿// <copyright file="WebRegisterRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the Register route.
    /// </summary>
    public sealed class WebRegisterRouteConfiguration
    {
        /// <summary>
        /// Determines whether the user should be automatically logged in after interacting with this route.
        /// </summary>
        /// <remarks>
        /// Auto login is possible only if the email verification feature is disabled
        /// on the default account store of the defined Stormpath application.
        /// <para>
        /// Configuration path: <c>stormpath.web.register.autoLogin</c>
        /// </para>
        /// </remarks>
        public bool AutoLogin { get; set; } = Default.Configuration.Web.Register.AutoLogin;

        /// <summary>
        /// The form configuration options.
        /// </summary>
        /// Configuration path: <c>stormpath.web.register.form</c>
        public WebRegisterRouteFormConfiguration Form { get; set; }

        /// <summary>
        /// The view to use for this route, or <see langword="null"/> to use the default view.
        /// </summary>
        /// Configuration path: <c>stormpath.web.register.view</c>
        public string View { get; set; } = Default.Configuration.Web.Register.View;

        /// <summary>
        /// The URI to redirect to if the operation is successful.
        /// </summary>
        /// Configuration path: <c>stormpath.web.register.nextUri</c>
        public string NextUri { get; set; } = Default.Configuration.Web.Register.NextUri;

        /// <summary>
        /// Determines whether the Register route is enabled.
        /// </summary>
        /// Configuration path: <c>stormpath.web.register.enabled</c>
        public bool Enabled { get; set; } = Default.Configuration.Web.Register.Enabled;

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// Configuration path: <c>stormpath.web.register.uri</c>
        public string Uri { get; set; } = Default.Configuration.Web.Register.Uri;

        /// <summary>
        /// Whether new accounts must verify their email address before becoming active.
        /// </summary>
        /// Configuration path: <c>okta.web.register.emailVerificationRequired</c>
        public bool EmailVerificationRequired { get; set; } = Default.Configuration.Web.Register.EmailVerificationRequired;
    }
}