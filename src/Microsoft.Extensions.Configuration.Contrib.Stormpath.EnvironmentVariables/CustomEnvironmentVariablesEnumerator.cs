// <copyright file="CustomEnvironmentVariablesEnumerator.cs" company="Stormpath, Inc.">
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
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.EnvironmentVariables
{
    public class CustomEnvironmentVariablesEnumerator
    {
        private readonly string prefix;
        private readonly string separator;

        public CustomEnvironmentVariablesEnumerator(string prefix, string separator)
        {
            this.prefix = prefix;
            this.separator = separator;
        }

        public IEnumerable<KeyValuePair<string, string>> GetItems(IDictionary environmentVariables)
        {
            foreach (DictionaryEntry item in environmentVariables)
            {
                var key = item.Key
                    .ToString()
                    .ToLower();

                if (!string.IsNullOrEmpty(this.prefix))
                {
                    if (!key.StartsWith(this.prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Trim prefix
                    key = key.Substring(this.prefix.Length);

                    // Trim stray beginning separator, if necessary
                    if (key.StartsWith(this.separator, StringComparison.OrdinalIgnoreCase))
                    {
                        key = key.Substring(this.separator.Length);
                    }
                }

                key = key.Replace(separator.ToLower(), Constants.KeyDelimiter);

                yield return new KeyValuePair<string, string>(key, item.Value.ToString());
            }
        }
    }
}
