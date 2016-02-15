// <copyright file="Validating.cs" company="Stormpath, Inc.">
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
using FluentAssertions;
using Stormpath.Configuration.Abstractions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Validating
    {
        [Fact]
        public void Throws_for_missing_key_id()
        {
            var anon = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = string.Empty,
                        secret = "secret!",
                    }
                }
            };

            Action act = () => ConfigurationLoader.Load(anon);

            act.ShouldThrow<ConfigurationException>().WithMessage("API key ID and secret is required.");
        }

        [Fact]
        public void Throws_for_missing_key_secret()
        {
            var anon = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "foobar",
                        secret = string.Empty,
                    }
                }
            };

            Action act = () => ConfigurationLoader.Load(anon);

            act.ShouldThrow<ConfigurationException>().WithMessage("API key ID and secret is required.");
        }

        [Fact]
        public void Throws_for_malformed_app_href()
        {
            var anon = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "foobar",
                        secret = "secret123!",
                    }
                },
                application = new
                {
                    href = "https://foo.bar/myapps/123",
                }
            };

            Action act = () => ConfigurationLoader.Load(anon);

            act.ShouldThrow<ConfigurationException>().Which.Message.Should().EndWith("is not a valid Stormpath Application href.");
        }

        [Fact]
        public void Blank_app_href_is_ok()
        {
            var anon = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "foobar",
                        secret = "secret123!",
                    }
                },
                application = new
                {
                    href = string.Empty,
                }
            };

            Action act = () => ConfigurationLoader.Load(anon);

            act.ShouldNotThrow();
        }
    }
}
