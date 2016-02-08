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
using System.Collections.Generic;
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
                VisitProperty(property, obj);
                VisitedProperty(property);
            }
        }

        protected virtual void VisitProperty(PropertyInfo property, object obj)
        {
            var propertyTypeInfo = property.PropertyType.GetTypeInfo();

            if (IsSupportedPrimitive(propertyTypeInfo))
            {
                VisitPrimitive(property.GetValue(obj));
            }
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(propertyTypeInfo))
            {
                VisitEnumerable(property.GetValue(obj) as IEnumerable);
            }
            else if (propertyTypeInfo.IsClass)
            {
                VisitObject(property.GetValue(obj));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected virtual void VisitedProperty(PropertyInfo property)
        {
            // Do nothing.
        }

        protected virtual void VisitPrimitive(object primitiveValue)
        {
            // Do nothing.
        }

        protected virtual void VisitEnumerable(IEnumerable enumerable)
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
