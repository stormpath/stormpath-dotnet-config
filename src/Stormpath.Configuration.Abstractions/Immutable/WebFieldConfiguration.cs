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

namespace Stormpath.Configuration.Abstractions.Immutable
{
    /// <summary>
    /// Represents configuration options for a field on a form.
    /// </summary>
    public sealed class WebFieldConfiguration
    {
        public WebFieldConfiguration(
            bool enabled,
            string label,
            string placeholder,
            bool required,
            string type)
        {
            this.Enabled = enabled;
            this.Label = label;
            this.Placeholder = placeholder;
            this.Required = required;
            this.Type = type;
        }

        public WebFieldConfiguration(WebFieldConfiguration existing)
            : this(enabled: existing?.Enabled ?? true,
                  label: existing?.Label,
                  placeholder: existing?.Placeholder,
                  required: existing?.Required ?? true,
                  type: existing?.Type)
        {
        }

        internal WebFieldConfiguration()
        {
        }

        /// <summary>
        /// Determines whether this field is enabled (shown).
        /// </summary>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// The label.
        /// </summary>
        public string Label { get; internal set; }

        /// <summary>
        /// The placeholder.
        /// </summary>
        public string Placeholder { get; internal set; }

        /// <summary>
        /// Determines whether the field is required.
        /// </summary>
        public bool Required { get; internal set; }

        /// <summary>
        /// The field type.
        /// </summary>
        public string Type { get; internal set; }
    }
}
