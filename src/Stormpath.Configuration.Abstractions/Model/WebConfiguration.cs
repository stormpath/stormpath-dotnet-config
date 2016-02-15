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
    public sealed class WebConfiguration
    {
        public string BasePath { get; set; }

        public WebOauth2RouteConfiguration Oauth2 { get; set; } = new WebOauth2RouteConfiguration();

        public Dictionary<string, bool> Expand { get; set; } = new Dictionary<string, bool>();

        public WebOauth2TokenCookieConfiguration AccessTokenCookie { get; set; } = new WebOauth2TokenCookieConfiguration();

        public WebOauth2TokenCookieConfiguration RefreshTokenCookie { get; set; } = new WebOauth2TokenCookieConfiguration();

        public List<string> Produces { get; set; } = new List<string>();

        public WebRegisterRouteConfiguration Register { get; set; } = new WebRegisterRouteConfiguration();

        public WebVerifyEmailRouteConfiguration VerifyEmail { get; set; } = new WebVerifyEmailRouteConfiguration();

        public WebLoginRouteConfiguration Login { get; set; } = new WebLoginRouteConfiguration();

        public WebLogoutRouteConfiguration Logout { get; set; } = new WebLogoutRouteConfiguration();

        public WebForgotPasswordRouteConfiguration ForgotPassword { get; set; } = new WebForgotPasswordRouteConfiguration();

        public WebChangePasswordRouteConfiguration ChangePassword { get; set; } = new WebChangePasswordRouteConfiguration();

        public WebIdSiteRouteConfiguration IdSite { get; set; } = new WebIdSiteRouteConfiguration();

        public WebSocialProvidersConfiguration SocialProviders { get; set; } = new WebSocialProvidersConfiguration();

        public WebMeRouteConfiguration Me { get; set; } = new WebMeRouteConfiguration();

        public WebSpaConfiguration Spa { get; set; } = new WebSpaConfiguration();

        public WebUnauthorizedConfiguration Unauthorized { get; set; } = new WebUnauthorizedConfiguration();
    }
}
