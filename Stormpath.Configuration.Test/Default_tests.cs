// <copyright file="Default_tests.cs" company="Stormpath, Inc.">
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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Default_tests
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] { new DefaultConfigTestCases.YamlTestCase() };
        }

        public Default_tests()
        {
            Cleanup();
        }

        private static void Cleanup()
        {
            // Clean up any stray items
            foreach (var entry in TestCases())
            {
                var testCase = entry[0] as DefaultConfigTestCases.TestCaseBase;
                File.Delete(testCase.Filename);
            }
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Loading_Stormpath_defaults(DefaultConfigTestCases.TestCaseBase testCase)
        {
            File.WriteAllText(testCase.Filename, testCase.FileContents);

            var config = StormpathConfiguration.Load();

            // Client section
            config.Client.ApiKey.File.Should().BeNullOrEmpty();
            config.Client.ApiKey.Id.Should().BeNullOrEmpty();
            config.Client.ApiKey.Secret.Should().BeNullOrEmpty();

            config.Client.CacheManager.DefaultTtl.Should().Be(300);
            config.Client.CacheManager.DefaultTti.Should().Be(300);
            config.Client.CacheManager.Caches.Should().HaveCount(1);
            config.Client.CacheManager.Caches.Single().Key.Should().Be("account");
            config.Client.CacheManager.Caches.Single().Value.Ttl.Should().Be(300);
            config.Client.CacheManager.Caches.Single().Value.Tti.Should().Be(300);

            config.Client.BaseUrl.Should().Be("https://api.stormpath.com/v1");
            config.Client.ConnectionTimeout.Should().Be(30);
            config.Client.AuthenticationScheme.Should().Be(ClientAuthenticationScheme.SAuthc1);

            config.Client.Proxy.Port.Should().Be(null);
            config.Client.Proxy.Host.Should().BeNullOrEmpty();
            config.Client.Proxy.Username.Should().BeNullOrEmpty();
            config.Client.Proxy.Password.Should().BeNullOrEmpty();

            // Application section
            config.Application.Href.Should().BeNullOrEmpty();
            config.Application.Name.Should().BeNullOrEmpty();

            // Web section

            Cleanup();
        }
    }
}
