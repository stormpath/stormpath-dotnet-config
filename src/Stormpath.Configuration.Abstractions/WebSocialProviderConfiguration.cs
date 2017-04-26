// <copyright file="WebSocialProviderConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for a social login provider.
    /// </summary>
    public class WebSocialProviderConfiguration
    {
        /// <summary>
        /// The name rendered in the UI for this provider.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.social.[providerId].displayName</c></remarks>
        public string DisplayName { get; set; }

        /// <summary>
        /// The scope requested of this social provider.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.social.[providerId].scope</c></remarks>
        public string Scope { get; set; }
    }
}
