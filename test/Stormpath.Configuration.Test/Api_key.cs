// <copyright file="Api_key.cs" company="Stormpath, Inc.">
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

using FluentAssertions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Api_key
    {
        [Fact]
        public void Is_mapped_to_client_root()
        {
            var supplied = new
            {
                apiKey = new
                {
                    id = "foobar",
                    secret = "secret123!"
                }
            };

            var config = StormpathConfiguration.Load(supplied);

            config.Client.ApiKey.Id.Should().Be("foobar");
            config.Client.ApiKey.Secret.Should().Be("secret123!");
        }
    }
}
