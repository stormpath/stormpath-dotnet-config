// <copyright file="WebVerifyEmailRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the Verify Email route.
    /// </summary>
    public sealed class WebVerifyEmailRouteConfiguration
    {
        public WebVerifyEmailRouteConfiguration(
            string view,
            string nextUri,
            bool? enabled,
            string uri)
        {
            this.View = view;
            this.NextUri = nextUri;
            this.Enabled = enabled;
            this.Uri = uri;
        }

        public WebVerifyEmailRouteConfiguration(WebVerifyEmailRouteConfiguration existing)
            : this(view: existing.View,
                  nextUri: existing.NextUri,
                  enabled: existing.Enabled,
                  uri: existing.Uri)
        {
        }

        internal WebVerifyEmailRouteConfiguration()
        {
        }

        /// <summary>
        /// The view to use for this route, or <see langword="null"/> to use the default view.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.verifyEmail.view</c></remarks>
        public string View { get; internal set; }

        /// <summary>
        /// The URI to redirect to if the operation is successful.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.verifyEmail.nextUri</c></remarks>
        public string NextUri { get; internal set; }

        /// <summary>
        /// Determines whether the Change Password route is enabled.
        /// </summary>
        /// <remarks>
        /// Unless explicitly set to <see langword="false"/>, the email
        /// verification feature will be automatically enabled if the default account
        /// store for the defined Stormpath application has the email verification workflow enabled.
        /// <para>
        /// Configuration path: <c>stormpath.web.verifyEmail.enabled</c>
        /// </para>
        /// </remarks>
        public bool? Enabled { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.verifyEmail.uri</c></remarks>
        public string Uri { get; internal set; }
    }
}