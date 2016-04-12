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
#if !NET451
using Microsoft.Extensions.PlatformAbstractions;
#endif

namespace Stormpath.Configuration
{
    public static class HomePath
    {
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

        // Copied from DNX's DnuEnvironment.cs
        public static string GetHomePath()
        {
#if NET451
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
