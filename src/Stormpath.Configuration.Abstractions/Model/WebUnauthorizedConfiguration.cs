// <copyright file="WebUnauthorizedConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for handling unauthorized access.
    /// </summary>
    public sealed class WebUnauthorizedConfiguration
    {
        public WebUnauthorizedConfiguration(string view)
        {
            this.View = view;
        }

        public WebUnauthorizedConfiguration(WebUnauthorizedConfiguration existing)
            : this(view: existing.View)
        {
        }

        internal WebUnauthorizedConfiguration()
        {
        }

        /// <summary>
        /// The view to use when a user attempts to access an unauthorized route.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.unauthorized.view</c></remarks>
        public string View { get; internal set; }
    }
}