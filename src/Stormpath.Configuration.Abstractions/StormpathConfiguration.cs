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

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Represents a Stormpath Client configuration.
    /// </summary>
    /// <remarks>
    /// This is a strongly-typed version of the Stormpath configuration format.
    /// </remarks>
    /// <see href="https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md"/>
    public class StormpathConfiguration
    {
        /// <summary>
        /// The framework integration-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.web</c></remarks>
        public WebConfiguration Web { get; set; }

        /// <summary>
        /// The Okta API token.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.apiToken</c></remarks>
        public string ApiToken { get; set; }

        /// <summary>
        /// The Okta organization href.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.org</c></remarks>
        public string Org { get; set; }

        /// <summary>
        /// The Okta Application configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>okta.application</c></remarks>
        public OktaApplicationConfiguration Application { get; set; }
    }
}
