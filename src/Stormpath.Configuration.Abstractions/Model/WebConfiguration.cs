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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Stormpath.Configuration.Abstractions.Model
{
    /// <summary>
    /// Represents web framework integration configuration options.
    /// </summary>
    public sealed class WebConfiguration
    {
        public WebConfiguration(
            string basePath,
            WebOauth2RouteConfiguration oauth2Route,
            IDictionary<string, bool> expand,
            WebCookieConfiguration accessTokenCookie,
            WebCookieConfiguration refreshTokenCookie,
            IList<string> produces,
            WebRegisterRouteConfiguration registerRoute,
            WebVerifyEmailRouteConfiguration verifyRoute,
            WebLoginRouteConfiguration loginRoute,
            WebLogoutRouteConfiguration logoutRoute,
            WebForgotPasswordRouteConfiguration forgotPasswordRoute,
            WebChangePasswordRouteConfiguration changePasswordRoute,
            WebIdSiteRouteConfiguration idSiteRoute,
            IDictionary<string, WebSocialProviderConfiguration> social,
            WebMeRouteConfiguration meRoute,
            WebSpaConfiguration spa,
            WebUnauthorizedConfiguration unauthorizedRoute)
        {
            this.BasePath = basePath;
            this.Oauth2 = new WebOauth2RouteConfiguration(oauth2Route);
            this.Expand = new Dictionary<string, bool>(expand ?? new Dictionary<string, bool>(), StringComparer.OrdinalIgnoreCase);
            this.AccessTokenCookie = new WebCookieConfiguration(accessTokenCookie);
            this.RefreshTokenCookie = new WebCookieConfiguration(refreshTokenCookie);
            this.Produces = new List<string>(produces ?? new List<string>());
            this.Register = new WebRegisterRouteConfiguration(registerRoute);
            this.VerifyEmail = new WebVerifyEmailRouteConfiguration(verifyRoute);
            this.Login = new WebLoginRouteConfiguration(loginRoute);
            this.Logout = new WebLogoutRouteConfiguration(logoutRoute);
            this.ForgotPassword = new WebForgotPasswordRouteConfiguration(forgotPasswordRoute);
            this.ChangePassword = new WebChangePasswordRouteConfiguration(changePasswordRoute);
            this.IdSite = new WebIdSiteRouteConfiguration(idSiteRoute);
            this.Social = new Dictionary<string, WebSocialProviderConfiguration>(social ?? new Dictionary<string, WebSocialProviderConfiguration>());
            this.Me = new WebMeRouteConfiguration(meRoute);
            this.Spa = new WebSpaConfiguration(spa);
            this.Unauthorized = new WebUnauthorizedConfiguration(unauthorizedRoute);
        }

        public WebConfiguration(WebConfiguration existing)
            : this(existing.BasePath,
                  existing.Oauth2,
                  existing.Expand.ToDictionary(),
                  existing.AccessTokenCookie,
                  existing.RefreshTokenCookie,
                  existing.Produces.ToList(),
                  existing.Register,
                  existing.VerifyEmail,
                  existing.Login,
                  existing.Logout,
                  existing.ForgotPassword,
                  existing.ChangePassword,
                  existing.IdSite,
                  existing.Social.ToDictionary(),
                  existing.Me,
                  existing.Spa,
                  existing.Unauthorized)
        {
        }

        internal WebConfiguration()
        {
        }

        /// <summary>
        /// The web application's base path.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.basePath</c></remarks>
        public string BasePath { get; internal set; }

        /// <summary>
        /// The OAuth2 route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2</c></remarks>
        public WebOauth2RouteConfiguration Oauth2 { get; internal set; }

        /// <summary>
        /// The expansion options configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.expand</c></remarks>
        public IReadOnlyDictionary<string, bool> Expand { get; internal set; }

        /// <summary>
        /// The access token cookie configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.accessTokenCookie</c></remarks>
        public WebCookieConfiguration AccessTokenCookie { get; internal set; }

        /// <summary>
        /// The refresh token cookie configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.refreshTokenCookie</c></remarks>
        public WebCookieConfiguration RefreshTokenCookie { get; internal set; }

        /// <summary>
        /// The output types configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.produces</c></remarks>
        public IReadOnlyList<string> Produces { get; internal set; }

        /// <summary>
        /// The Register route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.register</c></remarks>
        public WebRegisterRouteConfiguration Register { get; internal set; }

        /// <summary>
        /// The Verify Email route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.verifyEmail</c></remarks>
        public WebVerifyEmailRouteConfiguration VerifyEmail { get; internal set; }

        /// <summary>
        /// The Login route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login</c></remarks>
        public WebLoginRouteConfiguration Login { get; internal set; }

        /// <summary>
        /// The Logout route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.logout</c></remarks>
        public WebLogoutRouteConfiguration Logout { get; internal set; }

        /// <summary>
        /// The Forgot Password route configuration
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.forgotPassword</c></remarks>
        public WebForgotPasswordRouteConfiguration ForgotPassword { get; internal set; }

        /// <summary>
        /// The Change Password route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.changePassword</c></remarks>
        public WebChangePasswordRouteConfiguration ChangePassword { get; internal set; }

        /// <summary>
        /// The ID Site route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite</c></remarks>
        public WebIdSiteRouteConfiguration IdSite { get; internal set; }

        /// <summary>
        /// The social providers configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.social</c></remarks>
        public IReadOnlyDictionary<string, WebSocialProviderConfiguration> Social { get; internal set; }

        /// <summary>
        /// The Me route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.me</c></remarks>
        public WebMeRouteConfiguration Me { get; internal set; }

        /// <summary>
        /// The Single-Page Application configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.spa</c></remarks>
        public WebSpaConfiguration Spa { get; internal set; }

        /// <summary>
        /// The Unauthorized route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.unauthorized</c></remarks>
        public WebUnauthorizedConfiguration Unauthorized { get; internal set; }
    }
}
