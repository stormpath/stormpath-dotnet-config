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
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml.Visitor
{
    public class YamlContextAwareVisitor : YamlVisitorBase
    {
        private readonly Stack<string> context;

        public YamlContextAwareVisitor(Stack<string> context = null)
        {
            this.context = context == null
                ? new Stack<string>()
                : new Stack<string>(context.Reverse());
        }

        public IList<KeyValuePair<string, string>> Items { get; } = new List<KeyValuePair<string, string>>();

        protected override void VisitPair(YamlNode key, YamlNode value)
        {
            EnterContext((YamlScalarNode)key);
            base.Visit(value);
            ExitContext();
        }

        protected override void Visit(YamlScalarNode scalar)
        {
            var key = string.Join(Constants.KeyDelimiter, context.Reverse());
            string value = string.Empty;

            if (!IsNull(scalar))
            {
                value = scalar.Value;
            }

            this.Items.Add(new KeyValuePair<string, string>(key, value));
        }

        protected override void Visit(YamlMappingNode mapping)
        {
            var nestedVisitor = new YamlContextAwareVisitor(context);
            nestedVisitor.VisitChildren(mapping);
            
            foreach (var item in nestedVisitor.Items)
            {
                this.Items.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            }
        }

        protected override void Visit(YamlSequenceNode sequence)
        {
            var nestedVisitor = new YamlContextAwareSequenceVisitor(context);
            nestedVisitor.Visit(sequence);

            foreach (var item in nestedVisitor.Items)
            {
                this.Items.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            }
        }

        private void EnterContext(YamlScalarNode scalar)
        {
            // add support for null keys here
            context.Push(scalar.Value);
        }

        private void ExitContext()
        {
            context.Pop();
        }

        private static bool IsNull(YamlScalarNode scalar)
        {
            if (string.IsNullOrEmpty(scalar.Value))
            {
                return true;
            }

            if (string.Equals(scalar.Tag, "tag:yaml.org,2002:null", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(scalar.Value, "null", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(scalar.Value, "~", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
