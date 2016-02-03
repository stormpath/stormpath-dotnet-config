// <copyright file="StormpathConfiguration.cs" company="Stormpath, Inc.">
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

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.PropertiesFile
{
    internal sealed class PropertiesFileParser
    {
        private readonly Stream stream;

        public PropertiesFileParser(Stream stream)
        {
            this.stream = stream;
        }

        public IEnumerable<KeyValuePair<string, string>> Parse()
        {
            using (var reader = new StreamReader(stream))
            {
                var sectionPrefix = string.Empty;

                while (reader.Peek() != -1)
                {
                    var rawLine = reader.ReadLine();
                    var line = rawLine.Trim();

                    // Ignore blank lines
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    // Ignore comments
                    if (line[0] == '!' || line[0] == '#')
                    {
                        continue;
                    }

                    var key = new StringBuilder();
                    var value = new StringBuilder();
                    bool foundSeparator = false;

                    for (int i = 0; i < line.Length; i++)
                    {

                    }

                    if (!foundSeparator)
                    {
                        throw new FormatException(string.Format(Resources.Error_UnrecognizedLineFormat, rawLine));
                    }

                    int separator = line.IndexOf('=');
                    if (separator < 0)
                    {

                    }

                    // key = value OR "value"
                    string key = sectionPrefix + line.Substring(0, separator).Trim();
                    string value = line.Substring(separator + 1).Trim();

                    // Remove quotes
                    if (value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"')
                    {
                        value = value.Substring(1, value.Length - 2);
                    }


                }
            }

        }
    }
}
