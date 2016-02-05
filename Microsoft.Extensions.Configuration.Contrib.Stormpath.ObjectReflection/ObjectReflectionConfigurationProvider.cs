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

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection
{
    public class ObjectReflectionConfigurationProvider : ConfigurationProvider
    {
        private readonly object sourceObject;

        public ObjectReflectionConfigurationProvider(object sourceObject)
        {
            this.sourceObject = sourceObject;
        }

        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var enumerator = new ObjectReflectionEnumerator();

            foreach (var pair in enumerator.GetItems(this.sourceObject))
            {
                if (data.ContainsKey(pair.Key))
                {
                    throw new FormatException(string.Format(Resources.Error_KeyIsDuplicated, pair.Key));
                }

                data[pair.Key] = pair.Value;
            }

            Data = data;
        }
    }
}
