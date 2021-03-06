﻿// <copyright file="WebOauth2TokenValidationStrategy.cs" company="Stormpath, Inc.">
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
    /// Represents the available OAuth 2.0 token validation strategies.
    /// </summary>
    public enum WebOauth2TokenValidationStrategy
    {
        /// <summary>
        /// Local validation.
        /// </summary>
        Local = 0,

        /// <summary>
        /// Stormpath (remote) validation.
        /// </summary>
        Stormpath = 1
    }
}