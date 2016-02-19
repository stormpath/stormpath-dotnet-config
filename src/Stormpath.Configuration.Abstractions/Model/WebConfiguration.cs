// <copyright file="WebConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents web framework integration configuration options.
    /// </summary>
    public sealed class WebConfiguration
    {
        /// <summary>
        /// The web application's base path.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.basePath</c></remarks>
        public string BasePath { get; set; }

        /// <summary>
        /// The OAuth2 route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2</c></remarks>
        public WebOauth2RouteConfiguration Oauth2 { get; set; } = new WebOauth2RouteConfiguration();

        /// <summary>
        /// The expansion options configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.expand</c></remarks>
        public Dictionary<string, bool> Expand { get; set; } = new Dictionary<string, bool>();

        /// <summary>
        /// The access token cookie configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.accessTokenCookie</c></remarks>
        public WebCookieConfiguration AccessTokenCookie { get; set; } = new WebCookieConfiguration();

        /// <summary>
        /// The refresh token cookie configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.refreshTokenCookie</c></remarks>
        public WebCookieConfiguration RefreshTokenCookie { get; set; } = new WebCookieConfiguration();

        /// <summary>
        /// The output types configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.produces</c></remarks>
        public List<string> Produces { get; set; } = new List<string>();

        /// <summary>
        /// The Register route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.register</c></remarks>
        public WebRegisterRouteConfiguration Register { get; set; } = new WebRegisterRouteConfiguration();

        /// <summary>
        /// The Verify Email route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.verifyEmail</c></remarks>
        public WebVerifyEmailRouteConfiguration VerifyEmail { get; set; } = new WebVerifyEmailRouteConfiguration();

        /// <summary>
        /// The Login route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login</c></remarks>
        public WebLoginRouteConfiguration Login { get; set; } = new WebLoginRouteConfiguration();

        /// <summary>
        /// The Logout route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.logout</c></remarks>
        public WebLogoutRouteConfiguration Logout { get; set; } = new WebLogoutRouteConfiguration();

        /// <summary>
        /// The Forgot Password route configuration
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.forgotPassword</c></remarks>
        public WebForgotPasswordRouteConfiguration ForgotPassword { get; set; } = new WebForgotPasswordRouteConfiguration();

        /// <summary>
        /// The Change Password route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.changePassword</c></remarks>
        public WebChangePasswordRouteConfiguration ChangePassword { get; set; } = new WebChangePasswordRouteConfiguration();

        /// <summary>
        /// The ID Site route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite</c></remarks>
        public WebIdSiteRouteConfiguration IdSite { get; set; } = new WebIdSiteRouteConfiguration();

        /// <summary>
        /// The social providers configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.socialProviders</c></remarks>
        public WebSocialProvidersConfiguration SocialProviders { get; set; } = new WebSocialProvidersConfiguration();

        /// <summary>
        /// The Me route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me</c></remarks>
        public WebMeRouteConfiguration Me { get; set; } = new WebMeRouteConfiguration();

        /// <summary>
        /// The Single-Page Application configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.spa</c></remarks>
        public WebSpaConfiguration Spa { get; set; } = new WebSpaConfiguration();

        /// <summary>
        /// The Unauthorized route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.unauthorized</c></remarks>
        public WebUnauthorizedConfiguration Unauthorized { get; set; } = new WebUnauthorizedConfiguration();
    }
}
