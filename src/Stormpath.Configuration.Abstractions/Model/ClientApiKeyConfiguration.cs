// <copyright file="ApiKeyConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents API Key configuration options.
    /// </summary>
    public sealed class ClientApiKeyConfiguration
    {
        public ClientApiKeyConfiguration(string file, string id, string secret)
        {
            this.File = file;
            this.Id = id;
            this.Secret = secret;
        }

        public ClientApiKeyConfiguration(ClientApiKeyConfiguration existing)
            : this(file: existing.File,
                  id: existing.Id,
                  secret: existing.Secret)
        {
        }

        internal ClientApiKeyConfiguration()
        {
        }

        /// <summary>
        /// An optional path to an <c>apiKey.properties</c> file.
        /// </summary>
        /// <remarks>
        /// If set, this will override any values stored in <see cref="Id"/> and <see cref="Secret"/>.
        /// <para>
        /// Configuration path: <c>stormpath.client.apiKey.file</c>
        /// </para>
        /// </remarks>
        public string File { get; internal set; }

        /// <summary>
        /// The API Key ID.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.apiKey.id</c></remarks>
        public string Id { get; internal set; }

        /// <summary>
        /// The API Key secret.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client.apiKey.secret</c></remarks>
        public string Secret { get; internal  set; }
    }
}
