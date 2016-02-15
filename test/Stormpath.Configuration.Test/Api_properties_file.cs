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
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Api_properties_file : IDisposable
    {
        private readonly static string PropertiesFilePath =
            Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "apiKey.properties");

        private readonly static string PropertiesFileContents = @"
apiKey.id = FOOBAR
apiKey.secret = bazquxsecret!";

        public Api_properties_file()
        {
            File.WriteAllText(PropertiesFilePath, PropertiesFileContents);
        }

        public void Dispose()
        {
            File.Delete(PropertiesFilePath);
        }

        [Fact]
        public void Loads_id_and_secret()
        {
            var config = ConfigurationLoader.Load();

            config.Client.ApiKey.Id.Should().Be("FOOBAR");
            config.Client.ApiKey.Secret.Should().Be("bazquxsecret!");
        }

        [Fact]
        public void Loads_specified_file()
        {
            var otherPropertiesFilePath = Path.Combine(
                PlatformServices.Default.Application.ApplicationBasePath, "myother_apiKey.properties");

            File.WriteAllText(
                otherPropertiesFilePath, 
                PropertiesFileContents.Replace("FOOBAR", "FOOBAZ").Replace("secret!", "SECRETZ"));

            var config = ConfigurationLoader.Load(userConfiguration: new
            {
                client = new
                {
                  apiKey = new
                    {
                        file = "myother_apiKey.properties"
                    }
                }
            });

            config.Client.ApiKey.Id.Should().Be("FOOBAZ");
            config.Client.ApiKey.Secret.Should().Be("bazquxSECRETZ");

            File.Delete(otherPropertiesFilePath);
        }

        [Fact]
        public void Throws_if_file_is_missing()
        {
            var otherPropertiesFilePath = Path.Combine(
                PlatformServices.Default.Application.ApplicationBasePath, "myother_apiKey.properties");


            // Clean up just in case a previous test run didn't
            File.Delete(otherPropertiesFilePath);

            Action load = () => ConfigurationLoader.Load(userConfiguration: new
            {
                client = new
                {
                    apiKey = new
                    {
                        file = "myother_apiKey.properties"
                    }
                }
            });

            load.ShouldThrow<FileNotFoundException>();
        }
    }
}
