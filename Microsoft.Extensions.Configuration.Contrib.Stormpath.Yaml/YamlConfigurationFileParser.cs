﻿// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
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
        private readonly string root;
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public YamlConfigurationFileParser(string root = null)
        {
            this.root = root;
        }

        public IDictionary<string, string> Parse(Stream input)
        {
            _data.Clear();
            var visitor = new ContextAwareVisitor();

            try
           { 
                var yamlStream = new YamlStream();

                using (var reader = new StreamReader(input))
                {
                    yamlStream.Load(reader);

                }

                if (!yamlStream.Documents.Any())
                {
                    return _data;
                }

                yamlStream.Accept(visitor);
            }
            catch (YamlDotNet.Core.YamlException e)
            {
                throw new FormatException(string.Format(Resources.Error_YAMLParseError), e);
            }

            foreach (var item in visitor.Items)
            {
                var key = item.Key;

                if (!string.IsNullOrEmpty(this.root))
                {
                    key = $"{this.root}{Constants.KeyDelimiter}{key}";
                }

                if (_data.ContainsKey(key))
                {
                    throw new FormatException(string.Format(Resources.Error_KeyIsDuplicated, key));
                }
                _data[key] = item.Value;
            }

            return _data;
        }
    }
}
