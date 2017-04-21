// <copyright file="WebOauth2RouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the OAuth2 route.
    /// </summary>
    public sealed class WebOauth2RouteConfiguration
    {
        public WebOauth2RouteConfiguration(
            WebOauth2ClientCredentialsGrantConfiguration clientCredentialsGrant = null,
            WebOauth2PasswordGrantConfiguration passwordGrant = null,
            bool? enabled = null,
            string uri = null)
        {
            Client_Credentials = clientCredentialsGrant;
            Password = passwordGrant;
            Enabled = enabled ?? false;
            Uri = uri;
        }

        internal WebOauth2RouteConfiguration()
        {
        }

        public WebOauth2RouteConfiguration DeepClone()
            => new WebOauth2RouteConfiguration(Client_Credentials.DeepClone(), Password.DeepClone(), Enabled, Uri);

        /// <summary>
        /// The <c>client_credentials</c> grant type configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.client_credentials</c></remarks>
        public WebOauth2ClientCredentialsGrantConfiguration Client_Credentials { get; internal set; }

        /// <summary>
        /// The <c>password</c> grant type configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.password</c></remarks>
        public WebOauth2PasswordGrantConfiguration Password { get; internal set; }

        /// <summary>
        /// Determines whether the route is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The URI for this route, or <see langword="null"/> to use the default URI.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}
