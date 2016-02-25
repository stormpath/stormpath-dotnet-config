﻿// <copyright file="WebLoginRouteFormConfiguration.cs" company="Stormpath, Inc.">
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Stormpath.Configuration.Abstractions.Model
{
    /// <summary>
    /// Represents configuration options for the Login route form.
    /// </summary>
    public sealed class WebLoginRouteFormConfiguration
    {
        public WebLoginRouteFormConfiguration(
            IDictionary<string, WebFieldConfiguration> fields = null,
            IList<string> fieldOrder = null)
        {
            this.Fields = new Dictionary<string, WebFieldConfiguration>(fields ?? Default.Configuration.Web.Login.Form.Fields.ToDictionary(), StringComparer.OrdinalIgnoreCase);
            this.FieldOrder = new List<string>(fieldOrder ?? Default.Configuration.Web.Login.Form.FieldOrder.ToList());
        }

        public WebLoginRouteFormConfiguration(WebLoginRouteFormConfiguration existing)
            : this(fields: existing?.Fields.ToDictionary(),
                  fieldOrder: existing?.FieldOrder.ToList())
        {
        }

        internal WebLoginRouteFormConfiguration()
        {
        }

        /// <summary>
        /// The field configuration options.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.form.fields</c></remarks>
        public IReadOnlyDictionary<string, WebFieldConfiguration> Fields { get; internal set; }

        /// <summary>
        /// The field order.
        /// </summary>
        /// <remarks>Configuration path: <c>stormpath.web.login.form.fieldOrder</c></remarks>
        public IReadOnlyList<string> FieldOrder { get; internal set; }
    }
}