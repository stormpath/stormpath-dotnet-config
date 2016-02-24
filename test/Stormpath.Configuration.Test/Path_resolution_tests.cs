// <copyright file="HomePathResolution_tests.cs" company="Stormpath, Inc.">
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

using System.IO;
using FluentAssertions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Path_resolution_tests
    {
        [Fact]
        public void With_segments()
        {
            var expected = Path.Combine(HomePath.GetHomePath(), ".stormpath", "apiKey.properties");

            HomePath.Resolve("~", ".stormpath", "apiKey.properties")
                .Should().Be(expected);
        }

        [Fact]
        public void With_full_path()
        {
            var expected = Path.Combine(HomePath.GetHomePath(), ".stormpath", "apiKey.properties");

            HomePath.Resolve("~\\.stormpath\\apiKey.properties")
                .Should().Be(expected);
        }

        [Fact]
        public void Without_tilde()
        {
            var expected = "C:\\MyApp\\apiKey.properties";

            HomePath.Resolve(expected)
                .Should().Be(expected);
        }
    }
}
