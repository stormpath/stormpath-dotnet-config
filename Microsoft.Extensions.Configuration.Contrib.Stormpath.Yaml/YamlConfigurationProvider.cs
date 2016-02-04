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

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml
{
    /// <summary>
    /// A YAML file based <see cref="ConfigurationProvider"/>.
    /// </summary>
    public class YamlConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance of <see cref="YamlConfigurationProvider"/>.
        /// </summary>
        /// <param name="path">Absolute path of the YAML configuration file.</param>
        public YamlConfigurationProvider(string path)
            : this(path, optional: false)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="YamlConfigurationProvider"/>.
        /// </summary>
        /// <param name="path">Absolute path of the YAML configuration file.</param>
        /// <param name="optional">Determines if the configuration is optional.</param>
        public YamlConfigurationProvider(string path, bool optional)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(Resources.Error_InvalidFilePath, nameof(path));
            }

            Optional = optional;
            Path = path;
        }

        /// <summary>
        /// Gets a value that determines if this instance of <see cref="JsonConfigurationProvider"/> is optional.
        /// </summary>
        public bool Optional { get; }

        /// <summary>
        /// The absolute path of the file backing this instance of <see cref="JsonConfigurationProvider"/>.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Loads the contents of the file at <see cref="Path"/>.
        /// </summary>
        /// <exception cref="FileNotFoundException">If <see cref="Optional"/> is <c>false</c> and a
        /// file does not exist at <see cref="Path"/>.</exception>
        public override void Load()
        {
            if (!File.Exists(Path))
            {
                if (Optional)
                {
                    Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    throw new FileNotFoundException(string.Format(Resources.Error_FileNotFound, Path), Path);
                }
            }
            else
            {
                using (var stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    Load(stream);
                }
            }
        }

        internal void Load(Stream stream)
        {
            YamlConfigurationFileParser parser = new YamlConfigurationFileParser();
            try
            {
                Data = parser.Parse(stream);
            }
            catch (Exception e) //todo
            {
                //string errorLine = string.Empty;
                //if (stream.CanSeek)
                //{
                //    stream.Seek(0, SeekOrigin.Begin);

                //    IEnumerable<string> fileContent;
                //    using (var streamReader = new StreamReader(stream))
                //    {
                //        fileContent = ReadLines(streamReader);
                //        errorLine = RetrieveErrorContext(e, fileContent);
                //    }
                //}

                //throw new FormatException(string.Format(Resources.Error_YAMLParseError, e.LineNumber, errorLine), e);
            }
        }

        //private static string RetrieveErrorContext(Exception e, IEnumerable<string> fileContent)
        //{
        //    string errorLine;
        //    if (e.LineNumber >= 2)
        //    {
        //        var errorContext = fileContent.Skip(e.LineNumber - 2).Take(2).ToList();
        //        errorLine = errorContext[0].Trim() + Environment.NewLine + errorContext[1].Trim();
        //    }
        //    else
        //    {
        //        var possibleLineContent = fileContent.Skip(e.LineNumber - 1).FirstOrDefault();
        //        errorLine = possibleLineContent ?? string.Empty;
        //    }

        //    return errorLine;
        //}

        //private static IEnumerable<string> ReadLines(StreamReader streamReader)
        //{
        //    string line;
        //    do
        //    {
        //        line = streamReader.ReadLine();
        //        yield return line;
        //    } while (line != null);
        //}
    }
}
