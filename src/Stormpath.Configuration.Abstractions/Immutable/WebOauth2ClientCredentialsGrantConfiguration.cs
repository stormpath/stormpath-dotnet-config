// <copyright file="WebOauth2ClientCredentialsGrantConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the OAuth2 <c>client_credentials</c> grant type.
    /// </summary>
    public sealed class WebOauth2ClientCredentialsGrantConfiguration
    {
        public WebOauth2ClientCredentialsGrantConfiguration(
            bool? enabled = null,
            WebOauth2TokenConfiguration accessToken = null)
        {
            this.Enabled = enabled ?? Default.Configuration.Web.Oauth2.Client_Credentials.Enabled;
            this.AccessToken = new WebOauth2TokenConfiguration(accessToken ?? Default.Configuration.Web.Oauth2.Client_Credentials.AccessToken);
        }

        public WebOauth2ClientCredentialsGrantConfiguration(WebOauth2ClientCredentialsGrantConfiguration existing)
            : this(enabled: existing?.Enabled,
                  accessToken: existing?.AccessToken)
        {
        }

        internal WebOauth2ClientCredentialsGrantConfiguration()
        {
        }

        /// <summary>
        /// Determines whether the <c>client_credentials</c> grant type is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.client_credentials.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The Access Token configuration options.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.client_credentials.accessToken</c></remarks>
        public WebOauth2TokenConfiguration AccessToken { get; internal set; }
    }
}