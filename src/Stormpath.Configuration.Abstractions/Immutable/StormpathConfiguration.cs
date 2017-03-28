﻿// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
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
            OktaConfiguration okta = null,
            WebConfiguration web = null)
        {
            this.Okta = new OktaConfiguration(okta ?? Default.Configuration.Okta);
            this.Web = new WebConfiguration(web ?? Default.Configuration.Web);
        }

        public StormpathConfiguration(StormpathConfiguration existing)
            : this(okta: existing?.Okta,
                  web: existing?.Web)
        {
        }

        internal StormpathConfiguration()
        {
        }

        /// <summary>
        /// The Okta-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.okta</c></remarks>
        public OktaConfiguration Okta { get; internal set; }

        /// <summary>
        /// The framework integration-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web</c></remarks>
        public WebConfiguration Web { get; internal set; }
    }
}
