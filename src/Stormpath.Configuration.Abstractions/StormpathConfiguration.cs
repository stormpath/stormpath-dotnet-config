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

using Stormpath.Configuration.Abstractions.Model;

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Represents a Stormpath Client configuration.
    /// </summary>
    /// <remarks>
    /// This is a strongly-typed version of the stormpath config file format.
    /// </remarks>
    /// <see href="https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md"/>
    public sealed class StormpathConfiguration
    {
        /// <summary>
        /// The Client-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.client</c></remarks>
        public ClientConfiguration Client { get; set; } = new ClientConfiguration();

        /// <summary>
        /// The Application-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.application</c></remarks>
        public ApplicationConfiguration Application { get; set; } //= new ApplicationConfiguration();

        /// <summary>
        /// The framework integration-specific configuration.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web</c></remarks>
        public WebConfiguration Web { get; set; } = new WebConfiguration();
    }
}
