// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents an immutable Stormpath Client configuration.
    /// </summary>
    /// <remarks>
    /// This is a strongly-typed version of the Stormpath configuration format.
    /// </remarks>
    /// <see href="https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md"/>
    public class StormpathConfiguration
    {
        public StormpathConfiguration(
            string apiToken = null,
            string org = null,
            string authorizationServerId = null,
            OktaApplicationConfiguration application = null,
            WebConfiguration web = null)
        {
            ApiToken = apiToken;
            Org = org;
            AuthorizationServerId = authorizationServerId;
            Application = application;
            Web = web;
        }

        internal StormpathConfiguration()
        {
        }

        public StormpathConfiguration DeepClone()
            => new StormpathConfiguration(ApiToken, Org, AuthorizationServerId, Application?.DeepClone(), Web.DeepClone());

        /// <summary>
        /// The Okta API token.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.apiToken</c></remarks>
        public string ApiToken { get; internal set; }

        /// <summary>
        /// The Okta organization href.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.org</c></remarks>
        public string Org { get; internal set; }

        /// <summary>
        /// The Okta authorization server ID.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.authorizationServerId</c></remarks>
        public string AuthorizationServerId { get; internal set; }

        /// <summary>
        /// The Okta application configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.application</c></remarks>
        public OktaApplicationConfiguration Application { get; internal set; }

        /// <summary>
        /// The framework integration-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.web</c></remarks>
        public WebConfiguration Web { get; internal set; }
    }
}
