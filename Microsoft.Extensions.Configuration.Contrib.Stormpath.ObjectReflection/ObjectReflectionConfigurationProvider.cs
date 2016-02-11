// <copyright file="ObjectReflectionConfigurationProvider.cs" company="Stormpath, Inc.">
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
using Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Visitors;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection
{
    public class ObjectReflectionConfigurationProvider : ConfigurationProvider
    {
        private readonly object sourceObject;
        private readonly string root;

        public ObjectReflectionConfigurationProvider(object sourceObject, string root)
        {
            this.sourceObject = sourceObject;
            this.root = root;
        }

        public override void Load()
        {
            var data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var items = ContextAwareObjectVisitor.Visit(this.sourceObject);

            foreach (var item in items)
            {
                var key = item.Key;

                if (!string.IsNullOrEmpty(this.root))
                {
                    key = $"{this.root}{Constants.KeyDelimiter}{key}";
                }

                if (data.ContainsKey(key))
                {
                    throw new FormatException(string.Format(Resources.Error_KeyIsDuplicated, key));
                }
                data[key] = item.Value;
            }

            Data = data;
        }
    }
}
