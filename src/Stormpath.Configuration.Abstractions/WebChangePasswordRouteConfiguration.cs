﻿// <copyright file="WebChangePasswordRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the Change Password route.
    /// </summary>
    public sealed class WebChangePasswordRouteConfiguration
    {
        /// <summary>
        /// The view to use for this route, or <see langword="null"/> to use the default view.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.changePassword.view</c></remarks>
        public string View { get; set; } = Default.Configuration.Web.ChangePassword.View;

        /// <summary>
        /// The URI to redirect to if an error occurs.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.changePassword.errorUri</c></remarks>
        public string ErrorUri { get; set; } = Default.Configuration.Web.ChangePassword.ErrorUri;

        /// <summary>
        /// The URI to redirect to if the operation is successful.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.changePassword.nextUri</c></remarks>
        public string NextUri { get; set; } = Default.Configuration.Web.ChangePassword.NextUri;

        /// <summary>
        /// Determines whether the Change Password route is enabled.
        /// </summary>
        /// <remarks>
        /// Unless explicitly set to <see langword="false"/>, this feature
        /// will be automatically enabled if the default account store for the defined
        /// Stormpath application has the password reset workflow enabled.
        /// <para>
        /// Configuration path: <c>stormpath.web.changePassword.enabled</c>
        /// </para>
        /// </remarks>
        public bool Enabled { get; set; } = Default.Configuration.Web.ChangePassword.Enabled;

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.changePassword.uri</c></remarks>
        public string Uri { get; set; } = Default.Configuration.Web.ChangePassword.Uri;
    }
}