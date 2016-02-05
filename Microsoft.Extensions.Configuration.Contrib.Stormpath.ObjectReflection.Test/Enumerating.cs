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

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Test
{
    public class Enumerating
    {
        [Fact]
        public void Null_object()
        {
            object @obj = null;

            var enumerator = new ObjectReflectionEnumerator();

            enumerator.GetItems(@obj).Should().BeEmpty();
        }

        [Fact]
        public void Simple_object()
        {
            var @obj = new SimpleTestClass()
            {
                Foo = "bar",
                Baz = 123
            };

            var enumerator = new ObjectReflectionEnumerator();

            var expected = new Dictionary<string, string>()
            {
                ["Foo"] = "bar",
                ["Baz"] = "123",
            };

            enumerator.GetItems(@obj).Should().Equal(expected);
        }

        [Fact]
        public void Complex_object()
        {
            var @obj = new NestedClass()
            {
                Qux = "foobar",
                Nest1 = new SimpleTestClass()
                {
                    Foo = "One",
                    Baz = 1
                },
                Nest2 = new SimpleTestClass()
                {
                    Foo = "Two",
                    Baz = 2
                }
            };

            var enumerator = new ObjectReflectionEnumerator();

            var expected = new Dictionary<string, string>()
            {
                ["Qux"] = "foobar",
                ["Nest1:Foo"] = "One",
                ["Nest1:Baz"] = "1",
                ["Nest2:Foo"] = "Two",
                ["Nest2:Baz"] = "2",
            };

            enumerator.GetItems(@obj).Should().Equal(expected);
        }

        [Fact]
        public void Simple_anonymous_object()
        {
            var anon = new { Foo = "bar", Baz = 123 };

            var enumerator = new ObjectReflectionEnumerator();

            var expected = new Dictionary<string, string>()
            {
                ["Foo"] = "bar",
                ["Baz"] = "123",
            };

            enumerator.GetItems(anon).Should().Equal(expected);
        }

        [Fact]
        public void Complex_anonymous_object()
        {
            var anon = new
            {
                Qux = "foobar",
                Nest1 = new
                {
                    Foo = "One",
                    Baz = 1
                },
                Nest2 = new
                {
                    Foo = "Two",
                    Baz = 2
                }
            };

            var enumerator = new ObjectReflectionEnumerator();

            var expected = new Dictionary<string, string>()
            {
                ["Qux"] = "foobar",
                ["Nest1:Foo"] = "One",
                ["Nest1:Baz"] = "1",
                ["Nest2:Foo"] = "Two",
                ["Nest2:Baz"] = "2",
            };

            enumerator.GetItems(anon).Should().Equal(expected);
        }
    }

    public class SimpleTestClass
    {
        public string Foo { get; set; }

        public int Baz { get; set; }
    }

    public class NestedClass
    {
        public string Qux { get; set; }

        public SimpleTestClass Nest1 { get; set; }

        public SimpleTestClass Nest2 { get; set; }
    }
}
