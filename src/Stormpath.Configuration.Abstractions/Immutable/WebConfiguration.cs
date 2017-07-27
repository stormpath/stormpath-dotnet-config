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

namespace Stormpath.Configuration.Abstractions.Immutable
{
    /// <summary>
    /// Represents web framework integration configuration options.
    /// </summary>
    public sealed class WebConfiguration
    {
        public WebConfiguration(
            string serverUri = null,
            string basePath = null,
            WebOauth2RouteConfiguration oauth2Route = null,
            WebCookieConfiguration accessTokenCookie = null,
            WebRefreshTokenCookieConfiguration refreshTokenCookie = null,
            IList<string> produces = null,
            WebRegisterRouteConfiguration registerRoute = null,
            WebVerifyEmailRouteConfiguration verifyRoute = null,
            WebLoginRouteConfiguration loginRoute = null,
            WebLogoutRouteConfiguration logoutRoute = null,
            WebForgotPasswordRouteConfiguration forgotPasswordRoute = null,
            WebChangePasswordRouteConfiguration changePasswordRoute = null,
            WebCallbackRouteConfiguration callbackRoute = null,
            IDictionary<string, WebSocialProviderConfiguration> social = null,
            WebMeRouteConfiguration meRoute = null)
        {
            ServerUri = serverUri;
            BasePath = basePath;
            Oauth2 = oauth2Route;
            AccessTokenCookie = accessTokenCookie;
            RefreshTokenCookie = refreshTokenCookie;
            Produces = produces.ToList();
            Register = registerRoute;
            VerifyEmail = verifyRoute;
            Login = loginRoute;
            Logout = logoutRoute;
            ForgotPassword = forgotPasswordRoute;
            ChangePassword = changePasswordRoute;
            Callback = callbackRoute;
            Social = new Dictionary<string, WebSocialProviderConfiguration>(social, StringComparer.OrdinalIgnoreCase);
            Me = meRoute;
        }

        internal WebConfiguration()
        {
        }

        public WebConfiguration DeepClone()
            => new WebConfiguration(
                ServerUri,
                BasePath,
                Oauth2.DeepClone(),
                AccessTokenCookie.DeepClone(),
                RefreshTokenCookie.DeepClone(),
                new List<string>(Produces),
                Register.DeepClone(),
                VerifyEmail.DeepClone(),
                Login.DeepClone(),
                Logout.DeepClone(),
                ForgotPassword.DeepClone(),
                ChangePassword.DeepClone(),
                Callback.DeepClone(),
                new Dictionary<string, WebSocialProviderConfiguration>(Social.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)),
                Me.DeepClone());

        /// <summary>
        /// The web server's base URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.serverUri</c></remarks>
        public string ServerUri { get; internal set; }

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
        /// The access token cookie configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.accessTokenCookie</c></remarks>
        public WebCookieConfiguration AccessTokenCookie { get; internal set; }

        /// <summary>
        /// The refresh token cookie configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.refreshTokenCookie</c></remarks>
        public WebRefreshTokenCookieConfiguration RefreshTokenCookie { get; internal set; }

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
        /// The Stormpath callback route configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.callback</c></remarks>
        public WebCallbackRouteConfiguration Callback { get; internal set; }

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
    }
}
