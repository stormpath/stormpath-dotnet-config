// <copyright file="WebRefeshTokenCookieConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for cookies produced by the framework.
    /// </summary>
    public class WebRefreshTokenCookieConfiguration : WebCookieConfiguration
    {
        public WebRefreshTokenCookieConfiguration(
            string name,
            bool httpOnly,
            bool? secure,
            string path,
            string domain,
            int? maxAge)
            : base(name, httpOnly, secure, path, domain)
        {
            MaxAge = maxAge;
        }

        internal WebRefreshTokenCookieConfiguration()
        {
        }

        public new WebRefreshTokenCookieConfiguration DeepClone()
            => new WebRefreshTokenCookieConfiguration(Name, HttpOnly, Secure, Path, Domain, MaxAge);

        /// <summary>
        /// The cookie max-age.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.refreshTokenCookie.maxAge</c></remarks>
        public int? MaxAge { get; internal set; }
    }

}
