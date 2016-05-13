// <copyright file="ReadOnlyDictionaryExtensions.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Provides extension methods for <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class ReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Creates a <see cref="IDictionary{TKey, TValue}"/> from a <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The dictionary key type.</typeparam>
        /// <typeparam name="TValue">The dictionary value type.</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source)
            => source?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
