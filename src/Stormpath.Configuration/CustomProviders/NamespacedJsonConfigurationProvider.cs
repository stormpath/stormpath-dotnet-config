// <copyright file="ConfigurationProviderNamepsaceDecorator.cs" company="Stormpath, Inc.">
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Stormpath.Configuration.CustomProviders
{
    public class NamespacedJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly string root;

        public NamespacedJsonConfigurationProvider(string path, bool optional, string root)
            : base(path, optional)
        {
            this.root = root;
        }

        public override void Load()
        {
            base.Load();

            if (string.IsNullOrEmpty(this.root))
            {
                return; // done!
            }

            var namespacedData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var entry in Data)
            {
                namespacedData.Add(DecorateKey(entry.Key), entry.Value);
            }

            Data = namespacedData;
        }

        private string DecorateKey(string key)
            => $"{root}{Constants.KeyDelimiter}{key}";
    }
}
