// <copyright file="WebSocialProvidersConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for social login providers.
    /// </summary>
    public sealed class WebSocialProvidersConfiguration
    {
        public WebSocialProvidersConfiguration(string callbackRoot)
        {
            this.CallbackRoot = callbackRoot;
        }

        public WebSocialProvidersConfiguration(WebSocialProvidersConfiguration existing)
            : this(callbackRoot: existing.CallbackRoot)
        {
        }

        internal WebSocialProvidersConfiguration()
        {
        }

        /// <summary>
        /// The callback root URI used for social login.
        /// </summary>
        /// Configuration path: <c>stormpath.web.socialProviders.callbackRoot</c>
        public string CallbackRoot { get; internal set; }
    }
}