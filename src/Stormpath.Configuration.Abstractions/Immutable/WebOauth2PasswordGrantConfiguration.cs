﻿// <copyright file="WebOauth2PasswordGrantConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for the OAuth2 <c>password</c> grant type.
    /// </summary>
    public sealed class WebOauth2PasswordGrantConfiguration
    {
        public WebOauth2PasswordGrantConfiguration(
            bool? enabled = null,
            string defaultScope = null,
            WebOauth2TokenValidationStrategy? validationStrategy = null)
        {
            Enabled = enabled ?? false;
            DefaultScope = defaultScope;
            ValidationStrategy = validationStrategy ?? WebOauth2TokenValidationStrategy.Local;
        }

        internal WebOauth2PasswordGrantConfiguration()
        {
        }

        public WebOauth2PasswordGrantConfiguration DeepClone()
            => new WebOauth2PasswordGrantConfiguration(Enabled, DefaultScope, ValidationStrategy);

        /// <summary>
        /// Determines whether the <c>password</c> grant type is enabled.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.password.enabled</c></remarks>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The default scope(s) that are requested in addition to any scopes requested by the client.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.oauth2.password.defaultScope</c></remarks>
        public string DefaultScope { get; internal set; }

        /// <summary>
        /// The selected validation strategy.
        /// </summary>
        public WebOauth2TokenValidationStrategy ValidationStrategy { get; internal set; }
    }
}