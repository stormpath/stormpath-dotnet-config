// <copyright file="ContextAwareObjectVisitor.cs" company="Stormpath, Inc.">
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Visitors
{
    public class ContextAwareObjectVisitor : ObjectVisitor
    {
        protected readonly Stack<string> context;
        protected readonly List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();

        public ContextAwareObjectVisitor(Stack<string> previousContext = null)
        {
            this.context = previousContext == null
                ? new Stack<string>()
                : new Stack<string>(previousContext.Reverse());
        }

        public static IEnumerable<KeyValuePair<string, string>> Visit(object obj)
        {
            var visitor = new ContextAwareObjectVisitor();
            visitor.VisitObject(obj);
            return visitor.items;
        }

        protected override void VisitProperty(PropertyInfo property, object obj)
        {
            EnterContext(property.Name);
            base.VisitProperty(property, obj);
        }

        protected override void VisitedProperty(PropertyInfo property)
        {
            ExitContext();
        }

        protected override void VisitPrimitive(object primitiveValue)
        {
            var key = string.Join(Constants.KeyDelimiter, context.Reverse());

            this.items.Add(new KeyValuePair<string, string>(key, primitiveValue.ToString()));
        }

        protected override void VisitEnumerable(IEnumerable enumerable)
        {
            var visitor = new ContextAwareEnumerableVisitor(this.context);
            visitor.VisitEnumerable(enumerable);
            this.items.AddRange(visitor.items);
        }

        protected void EnterContext(string context)
        {
            context = context ?? string.Empty;
            this.context.Push(context);
        }

        protected void ExitContext()
        {
            this.context.Pop();
        }
    }
}
