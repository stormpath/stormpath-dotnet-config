// <copyright file="CustomEnvironmentVariablesConfigurationProvider.cs" company="Stormpath, Inc.">
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

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.EnvironmentVariables
{
    public class CustomEnvironmentVariablesConfigurationProvider : ConfigurationProvider
    {
        private readonly string mustStartWith;
        private readonly string separator;
        private readonly string root;

        public CustomEnvironmentVariablesConfigurationProvider(string mustStartWith, string separator, string root)
        {
            this.mustStartWith = mustStartWith;
            this.separator = separator;
            this.root = root;
        }

        public override void Load()
        {
            var data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var enumerator = new CustomEnvironmentVariablesEnumerator(mustStartWith, separator);

            foreach (var item in enumerator.GetItems(Environment.GetEnvironmentVariables()))
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
