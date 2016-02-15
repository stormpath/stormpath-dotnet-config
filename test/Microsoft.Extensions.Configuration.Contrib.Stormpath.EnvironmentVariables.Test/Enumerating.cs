// <copyright file="Enumerating.cs" company="Stormpath, Inc.">
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
using FluentAssertions;
using Xunit;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.EnvironmentVariables.Test
{
    public class Enumerating
    {
        [Fact]
        public void Empty_dictionary_produces_empty_results()
        {
            var fakeEnvironmentVariables = new Dictionary<string, string>();

            var enumerator = new CustomEnvironmentVariablesEnumerator(prefix: string.Empty, separator: "_");

            enumerator.GetItems(fakeEnvironmentVariables).Should().BeEmpty();
        }

        [Fact]
        public void Ignores_items_without_prefix()
        {
            var fakeEnvironmentVariables = new Dictionary<string, object>()
            {
                ["foo_key_subkey1"] = 123,
                ["foo_key_subkey2"] = 456,
                ["qux_key_subkey3"] = "foo",
            };

            var enumerator = new CustomEnvironmentVariablesEnumerator(prefix: "bar", separator: "_");

            enumerator.GetItems(fakeEnvironmentVariables).Should().BeEmpty();
        }

        [Fact]
        public void Returns_everything_if_prefix_is_null()
        {
            var fakeEnvironmentVariables = new Dictionary<string, object>()
            {
                ["foo_key_subkey1"] = 123,
                ["foo_key_subkey2"] = 456,
                ["qux_key_subkey3"] = "foo",
            };

            var enumerator = new CustomEnvironmentVariablesEnumerator(prefix: null, separator: "_");

            var expected = new Dictionary<string, string>()
            {
                ["foo:key:subkey1"] = "123",
                ["foo:key:subkey2"] = "456",
                ["qux:key:subkey3"] = "foo",
            };

            enumerator.GetItems(fakeEnvironmentVariables).Should().Equal(expected);
        }

        [Fact]
        public void Removes_prefix_from_matched_items()
        {
            var fakeEnvironmentVariables = new Dictionary<string, object>()
            {
                ["foo_key_subkey1"] = 123,
                ["foo_key_subkey2"] = 456,
                ["qux_key_subkey3"] = "foo",
            };

            var enumerator = new CustomEnvironmentVariablesEnumerator(prefix: "foo", separator: "_");

            var expected = new Dictionary<string, string>()
            {
                ["key:subkey1"] = "123",
                ["key:subkey2"] = "456"
            };

            enumerator.GetItems(fakeEnvironmentVariables).Should().Equal(expected);
        }

        [Fact]
        public void With_custom_separator()
        {
            var fakeEnvironmentVariables = new Dictionary<string, object>()
            {
                ["foo//key//subkey1"] = 123,
                ["foo//key//subkey2"] = 456,
                ["qux//key//subkey3"] = "foo",
            };

            var enumerator = new CustomEnvironmentVariablesEnumerator(prefix: "foo", separator: "//");

            var expected = new Dictionary<string, string>()
            {
                ["key:subkey1"] = "123",
                ["key:subkey2"] = "456"
            };

            enumerator.GetItems(fakeEnvironmentVariables).Should().Equal(expected);
        }

        [Fact]
        public void Is_case_insensitive()
        {
            var fakeEnvironmentVariables = new Dictionary<string, object>()
            {
                ["foo_x_key_x_subkey1"] = 123,
                ["foo_x_key_x_subkey2"] = 456,
                ["qux_x_key_x_subkey3"] = "foo",
            };

            var enumerator = new CustomEnvironmentVariablesEnumerator(prefix: "FOO", separator: "_X_");

            var expected = new Dictionary<string, string>()
            {
                ["key:subkey1"] = "123",
                ["key:subkey2"] = "456",
            };

            enumerator.GetItems(fakeEnvironmentVariables).Should().Equal(expected);
        }
    }
}
