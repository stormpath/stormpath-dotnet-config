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

using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml
{
    public class YamlContextAwareSequenceVisitor : YamlVisitor
    {
        private readonly Stack<string> context;
        private int index = 0;

        public YamlContextAwareSequenceVisitor(Stack<string> context = null)
        {
            this.context = context ?? new Stack<string>();
        }

        public new void Visit(YamlSequenceNode sequence)
        {
            this.VisitChildren(sequence);
        }

        public IList<KeyValuePair<string, string>> Items { get; }
            = new List<KeyValuePair<string, string>>();

        public List<YamlNode> VisitedNodes
            = new List<YamlNode>();

        protected override void Visit(YamlScalarNode scalar)
        {
            var key = $"{string.Join(Constants.KeyDelimiter, context.Reverse())}{Constants.KeyDelimiter}{index}";

            this.Items.Add(new KeyValuePair<string, string>(key, scalar.Value));

            index++;
            this.VisitedNodes.Add(scalar);
        }
    }
}
