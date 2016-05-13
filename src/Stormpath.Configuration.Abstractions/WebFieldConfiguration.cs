// <copyright file="WebFieldConfiguration.cs" company="Stormpath, Inc.">
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
    /// Represents configuration options for a field on a form.
    /// </summary>
    public sealed class WebFieldConfiguration
    {

        /// <summary>
        /// Determines whether this field is enabled.
        /// </summary>
        /// <remarks>If a field is <b>not</b> enabled, the server will error if that field is submitted with a form.</remarks>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Determines whether this field is shown on the form.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// The label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The placeholder.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Determines whether the field is required.
        /// </summary>
        public bool Required { get; set; } = true;

        /// <summary>
        /// The field type.
        /// </summary>
        public string Type { get; set; }
    }
}
