// <copyright file="WebRefreshTokenCookieConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for refresh token cookies produced by the framework.
    /// </summary>
    public class WebRefreshTokenCookieConfiguration : WebCookieConfiguration
    {
        /// <summary>
        /// The Max-Age of the cookie in seconds. If <c>null</c>, the cookie will be saved as a Session cookie.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.refreshTokenCookie.maxAge</c></remarks>
        public int? MaxAge { get; set; }
    }
}
