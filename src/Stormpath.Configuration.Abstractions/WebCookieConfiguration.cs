// <copyright file="WebOauth2TokenCookieConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for cookies produced by the framework.
    /// </summary>
    public sealed class WebCookieConfiguration
    {
        /// <summary>
        /// The cookie name.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.[*Cookie].name</c></remarks>
        public string Name { get; set; }

        /// <summary>
        /// The cookie's <c>HttpOnly</c>flag.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.[*Cookie].httpOnly</c></remarks>
        public bool HttpOnly { get; set; } = true;

        /// <summary>
        /// The cookie's <c>Secure</c> flag.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.[*Cookie].secure</c></remarks>
        public bool? Secure { get; set; } = null;

        /// <summary>
        /// THe cookie path.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.[*Cookie].path</c></remarks>
        public string Path { get; set; }

        /// <summary>
        /// The cookie domain.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.[*Cookie].domain</c></remarks>
        public string Domain { get; set; }
    }
}