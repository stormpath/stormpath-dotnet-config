// <copyright file="WebIdSiteRouteConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for ID Site functionality.
    /// </summary>
    public sealed class WebIdSiteConfiguration
    {
        /// <summary>
        /// The relative path for the ID Site Login action, or <see langword="null"/> to use the default path.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.loginUri</c></remarks>
        public string LoginUri { get; set; }

        /// <summary>
        /// The relative path for the ID Site Forgot Password action, or <see langword="null"/> to use the default path.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.forgotUri</c></remarks>
        public string ForgotUri { get; set; }

        /// <summary>
        /// The relative path for the ID Site Register action, or <see langword="null"/> to use the default path.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.registerUri</c></remarks>
        public string RegisterUri { get; set; }

        /// <summary>
        /// Determines whether ID Site is used for login, registration, and password reset.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.idSite.enabled</c></remarks>
        public bool Enabled { get; set; }
    }
}