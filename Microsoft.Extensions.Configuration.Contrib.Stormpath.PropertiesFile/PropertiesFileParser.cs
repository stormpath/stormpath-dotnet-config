﻿// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
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
using System.IO;
using System.Text;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.PropertiesFile
{
    internal sealed class PropertiesFileParser
    {
        private readonly Stream stream;
        private readonly string root;

        public PropertiesFileParser(Stream stream, string root)
        {
            this.stream = stream;
            this.root = root;
        }

        public IEnumerable<KeyValuePair<string, string>> Parse()
        {
            using (var reader = new StreamReader(this.stream))
            {
                while (reader.Peek() != -1)
                {
                    var rawLine = reader.ReadLine();
                    
                    // Ignore blank lines
                    if (string.IsNullOrWhiteSpace(rawLine))
                    {
                        continue;
                    }

                    var line = rawLine.Trim();

                    // Ignore comments
                    if (line[0] == '!' || line[0] == '#')
                    {
                        continue;
                    }

                    var key = string.Empty;
                    var value = string.Empty;

                    int separator = line.IndexOf('=');
                    if (separator != -1)
                    {
                        key = line.Substring(0, separator).Trim();
                        value = line.Substring(separator + 1).Trim();
                    }

                    if (key.Contains(":"))
                    {
                        throw new FormatException(string.Format(Resources.Error_UnrecognizedLineFormat, rawLine));
                    }

                    key = key.Replace('.', ':');

                    if (!string.IsNullOrEmpty(this.root))
                    {
                        key = $"{this.root}:{key}";
                    }

                    yield return new KeyValuePair<string, string>(key, value);
                }
            }
        }
    }
}