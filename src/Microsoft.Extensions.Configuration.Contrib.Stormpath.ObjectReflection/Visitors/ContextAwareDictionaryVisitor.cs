// <copyright file="ContextAwareEnumerableVisitor.cs" company="Stormpath, Inc.">
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
using System.Reflection;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Visitors
{
    public sealed class ContextAwareDictionaryVisitor : ContextAwareObjectVisitor
    {
        public ContextAwareDictionaryVisitor(Stack<string> previousContext)
            : base(previousContext)
        {
        }

        protected override void VisitDictionary(IDictionary dictionary)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                VisitProperty(entry.Key.ToString(), entry.Value.GetType().GetTypeInfo(), entry.Value);
            }
        }
    }
}
