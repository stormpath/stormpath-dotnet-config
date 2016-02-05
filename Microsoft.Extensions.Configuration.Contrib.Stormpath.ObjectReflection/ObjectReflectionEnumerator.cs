// <copyright file="ObjectReflectionEnumerator.cs" company="Stormpath, Inc.">
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
using System.Reflection;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection
{
    public class ObjectReflectionEnumerator
    {
        private readonly Stack<string> context;

        public ObjectReflectionEnumerator(Stack<string> previousContext = null)
        {
            this.context = previousContext == null
                ? new Stack<string>()
                : new Stack<string>(previousContext.Reverse());
        }

        public IEnumerable<KeyValuePair<string, string>> GetItems(object sourceObject)
        {
            if (sourceObject == null)
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            return GetItemsInternal(sourceObject);
        }

        private IEnumerable<KeyValuePair<string, string>> GetItemsInternal(object sourceObject)
        {
            foreach (var prop in sourceObject.GetType().GetTypeInfo().DeclaredProperties)
            {
                this.context.Push(prop.Name);

                object value = prop.GetValue(sourceObject);

                bool isReferenceType =
                    prop.PropertyType.GetTypeInfo().IsClass
                    && prop.PropertyType != typeof(string);

                if (isReferenceType)
                {
                    var nestedEnumerator = new ObjectReflectionEnumerator(this.context);
                    foreach (var nestedItem in nestedEnumerator.GetItems(value))
                    {
                        yield return nestedItem;
                    }
                }
                else
                {
                    var key = string.Join(Constants.KeyDelimiter, context.Reverse());

                    yield return new KeyValuePair<string, string>(key, value.ToString());
                }

                this.context.Pop();
            }
        }
    }
}
