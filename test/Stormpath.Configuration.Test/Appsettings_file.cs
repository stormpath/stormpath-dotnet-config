// <copyright file="Appsettings_file.cs" company="Stormpath, Inc.">
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
    [Collection("I/O")]
    public class Appsettings_file : IDisposable
    {
        private readonly static string AppSettingsFilePath = Path.Combine(
            PlatformServices.Default.Application.ApplicationBasePath, "appsettings.json");

        private readonly static string AppSettingsFileContents = @"
{
  ""Logging"": {
    ""IncludeScopes"": false,
    ""LogLevel"": {
      ""Default"": ""Verbose"",
      ""System"": ""Information"",
      ""Microsoft"": ""Information""
    }
  },
  ""Stormpath"": {
    ""Client"": {
        ""ApiKey"": {
            ""Id"": ""appsetting-id"",
            ""Secret"": ""appsetting-secret123!""
        }
    },
    ""Application"": {
        ""Href"": ""https://api.stormpath.com/v1/applications/foobar123""
    }
  }
}
";

        public Appsettings_file()
        {
            File.WriteAllText(AppSettingsFilePath, AppSettingsFileContents);
        }

        public void Dispose()
        {
            File.Delete(AppSettingsFilePath);
        }

        [Fact]
        public void Loads_configuration()
        {
            var config = ConfigurationLoader.Initialize().Load();

            config.Client.ApiKey.Id.Should().Be("appsetting-id");
            config.Client.ApiKey.Secret.Should().Be("appsetting-secret123!");
            config.Application.Href.Should().Be("https://api.stormpath.com/v1/applications/foobar123");
        }
    }
}
