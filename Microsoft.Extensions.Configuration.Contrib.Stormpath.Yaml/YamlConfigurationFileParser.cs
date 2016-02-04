// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
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
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml.Visitor;
using YamlDotNet.RepresentationModel;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml
{
    internal class YamlConfigurationFileParser : YamlVisitor
    {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public IDictionary<string, string> Parse(Stream input)
        {
            _data.Clear();
            var yamlStream = new YamlStream();
            yamlStream.Load(new StreamReader(input));

            if (!yamlStream.Documents.Any())
            {
                return _data;
            }

            var visitor = new YamlContextAwareVisitor();
            yamlStream.Accept(visitor);

            foreach (var item in visitor.Items)
            {
                if (_data.ContainsKey(item.Key))
                {
                    throw new FormatException(string.Format(Resources.Error_KeyIsDuplicated, item.Key));
                }
                _data[item.Key] = item.Value;
            }

            return _data;
        }
    }
}
