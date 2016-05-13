// <copyright file="HomePath.cs" company="Stormpath, Inc.">
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
using System.Linq;
#if !NET45
using Microsoft.Extensions.PlatformAbstractions;
#endif

namespace Stormpath.Configuration
{
    /// <summary>
    /// Contains methods for resolving the home directory path.
    /// </summary>
    public static class HomePath
    {
        /// <summary>
        /// Resolves a collection of path segments with a home directory path.
        /// </summary>
        /// <remarks>
        /// Provides support for Unix-like paths on Windows. If the first path segment starts with <c>~</c>, this segment is prepended with the home directory path.
        /// </remarks>
        /// <param name="pathSegments">The path segments.</param>
        /// <returns>A combined path which includes the resolved home directory path (if necessary).</returns>
        public static string Resolve(params string[] pathSegments)
        {
            if (pathSegments.Length == 0)
            {
                return null;
            }

            if (!pathSegments[0].StartsWith("~"))
            {
                return Path.Combine(pathSegments);
            }

            var homePath = GetHomePath();

            var newSegments =
                new string[] { pathSegments[0].Replace("~", homePath) }
                .Concat(pathSegments.Skip(1))
                .ToArray();

            return Path.Combine(newSegments);
        }

        /// <summary>
        /// Resolves the current user's home directory path.
        /// </summary>
        /// <remarks>
        /// Copied from DNX's DnuEnvironment.cs
        /// </remarks>
        /// <returns></returns>
        public static string GetHomePath()
        {
#if NET45
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#else
            var runtimeEnv = PlatformServices.Default.Runtime;
            if (runtimeEnv.OperatingSystem == "Windows")
            {
                return Environment.GetEnvironmentVariable("USERPROFILE") ??
                    Environment.GetEnvironmentVariable("HOMEDRIVE") + Environment.GetEnvironmentVariable("HOMEPATH");
            }
            else
            {
                var home = Environment.GetEnvironmentVariable("HOME");

                if (string.IsNullOrEmpty(home))
                {
                    throw new Exception("Home directory not found. The HOME environment variable is not set.");
                }

                return home;
            }
#endif
        }
    }
}
