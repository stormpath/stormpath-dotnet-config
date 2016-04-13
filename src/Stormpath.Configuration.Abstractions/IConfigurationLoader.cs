// <copyright file="IConfigurationLoader.cs" company="Stormpath, Inc.">
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
    /// Loads the Stormpath configuration.
    /// </summary>
    public interface IConfigurationLoader
    {
        /// <summary>
        /// Loads the local Stormpath configuration and applies any user-defined configuration options.
        /// </summary>
        /// <param name="userConfiguration">
        /// An instance of <see cref="StormpathConfiguration"/>, or an anonymous type, containing user-defined configuration options.
        /// </param>
        /// <returns>The resolved Stormpath configuration.</returns>
        Immutable.StormpathConfiguration Load(object userConfiguration = null);
    }
}
