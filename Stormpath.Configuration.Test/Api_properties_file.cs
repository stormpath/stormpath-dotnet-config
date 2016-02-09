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
using Xunit;

namespace Stormpath.Configuration.Test
{
    [Collection("Disk IO Tests")]
    public class Api_properties_file
    {
        [Fact]
        public void Loads_id_and_secret()
        {
            var contents = @"
apiKey.id = FOOBAR
apiKey.secret = bazquxsecret!";

            File.WriteAllText("apiKey.properties", contents);

            var config = StormpathConfiguration.Load();

            config.Client.ApiKey.Id.Should().Be("FOOBAR");
            config.Client.ApiKey.Secret.Should().Be("bazquxsecret!");

            File.Delete("apiKey.properties");
        }

        [Fact(Skip = "when explicit configuration works")]
        public void Specified_file_is_loaded()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "when explicit configuration works")]
        public void Throws_if_file_is_missing()
        {
            throw new NotImplementedException();
        }
    }
}
