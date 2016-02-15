// <copyright file="NamespacedJsonConfigurationExtensions.cs" company="Stormpath, Inc.">
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
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Stormpath.Configuration.CustomProviders
{
    public static class NamespacedJsonConfigurationExtensions
    {
        /// <summary>
        /// Adds the JSON configuration provider at <paramref name="path"/> to <paramref name="configurationBuilder"/>.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="path">Absolute path or path relative to <see cref="IConfigurationBuilder.BasePath"/> of
        /// <paramref name="configurationBuilder"/>.</param>
        /// <param name="optional">Determines if loading the configuration provider is optional.</param>
        /// <param name="root">A root element to prepend to any discovered key.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">If <paramref name="optional"/> is <c>false</c> and the file cannot
        /// be resolved.</exception>
        public static IConfigurationBuilder AddJsonFile(
            this IConfigurationBuilder configurationBuilder,
            string path,
            bool optional,
            string root)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.", nameof(path));
            }

            var fullPath = Path.Combine(configurationBuilder.GetBasePath(), path);

            if (!optional && !File.Exists(fullPath))
            {
                throw new FileNotFoundException(string.Format("The configuration file '{0}' was not found and is not optional.", fullPath), fullPath);
            }

            configurationBuilder.Add(new NamespacedJsonConfigurationProvider(fullPath, optional: optional, root: root));

            return configurationBuilder;
        }
    }
}
