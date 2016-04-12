﻿// <copyright file="ApplicationConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents Application configuration options.
    /// </summary>
    public sealed class ApplicationConfiguration
    {
        public ApplicationConfiguration
            (string name = null,
            string href = null)
        {
            this.Name = name ?? Default.Configuration.Application.Name;
            this.Href = href ?? Default.Configuration.Application.Href;
        }

        public ApplicationConfiguration(ApplicationConfiguration existing)
            : this(name: existing?.Name,
                  href: existing?.Href)
        {
        }
       
        internal ApplicationConfiguration()
        {
        }

        /// <summary>
        /// The application's name.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.application.name</c></remarks>
        public string Name { get; internal set; }

        /// <summary>
        /// The application's Stormpath <c>href</c>.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.application.href</c></remarks>
        public string Href { get; internal set; }
    }
}