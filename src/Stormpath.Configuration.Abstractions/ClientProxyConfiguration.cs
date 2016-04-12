// <copyright file="ClientProxyConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents Client proxy configuration options.
    /// </summary>
    public sealed class ClientProxyConfiguration
    {
        /// <summary>
        /// The proxy port.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.proxy.port</c></remarks>
        public int? Port { get; set; }

        /// <summary>
        /// The proxy hostname.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.proxy.host</c></remarks>
        public string Host { get; set; }

        /// <summary>
        /// The proxy username.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.proxy.username</c></remarks>
        public string Username { get; set; }

        /// <summary>
        /// The proxy password.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.proxy.password</c></remarks>
        public string Password { get; set; }
    }
}
