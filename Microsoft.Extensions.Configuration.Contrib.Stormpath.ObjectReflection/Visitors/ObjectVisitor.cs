// <copyright file="ObjectVisitor.cs" company="Stormpath, Inc.">
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
using System.Reflection;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Visitors
{
    public abstract class ObjectVisitor
    {
        protected virtual void VisitObject(object obj)
        {
            if (obj != null)
            {
                VisitProperties(obj);
            }
        }

        protected virtual void VisitProperties(object obj)
        {
            foreach (var property in obj.GetType().GetTypeInfo().DeclaredProperties)
            {
                VisitProperty(property.Name, property.PropertyType.GetTypeInfo(), property.GetValue(obj));
            }
        }

        protected virtual void VisitProperty(string name, TypeInfo propertyTypeInfo, object actualValue)
        {
            if (IsSupportedPrimitive(propertyTypeInfo))
            {
                VisitPrimitive(actualValue);
            }
            else if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(propertyTypeInfo))
            {
                VisitDictionary(actualValue as IDictionary);
            }
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(propertyTypeInfo))
            {
                VisitEnumerable(actualValue as IEnumerable);
            }
            else if (propertyTypeInfo.IsClass)
            {
                VisitObject(actualValue);
            }
            else
            {
                throw new NotSupportedException($"The type '{propertyTypeInfo.Name}' is not supported at this position.");
            }
        }

        protected virtual void VisitPrimitive(object primitiveValue)
        {
            // Do nothing.
        }

        protected virtual void VisitEnumerable(IEnumerable enumerable)
        {
            // Do nothing.
        }

        protected virtual void VisitDictionary(IDictionary dictionary)
        {
            // Do nothing.
        }

        protected static bool IsSupportedPrimitive(TypeInfo typeInfo)
        {
            return typeInfo.IsPrimitive
                || typeInfo.IsEnum
                || typeInfo == typeof(string).GetTypeInfo()
                || IsNullable(typeInfo.AsType());
        }

        private static bool IsNullable(Type possiblyNullable)
            => Nullable.GetUnderlyingType(possiblyNullable) != null;
    }
}
