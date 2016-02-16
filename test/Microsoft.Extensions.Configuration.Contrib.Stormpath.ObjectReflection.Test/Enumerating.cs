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
using Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Visitors;
using Xunit;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Test
{
    public class Enumerating
    {
        [Fact]
        public void Null_object()
        {
            object @obj = null;

            ContextAwareObjectVisitor.Visit(@obj).Should().BeEmpty();
        }

        [Fact]
        public void Simple_object()
        {
            var @obj = new SimpleTestClass()
            {
                Foo = "bar",
                Baz = 123
            };

            var expected = new Dictionary<string, string>()
            {
                ["Foo"] = "bar",
                ["Baz"] = "123",
            };

            ContextAwareObjectVisitor.Visit(@obj).Should().Equal(expected);
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

            var expected = new Dictionary<string, string>()
            {
                ["Qux"] = "foobar",
                ["Nest1:Foo"] = "One",
                ["Nest1:Baz"] = "1",
                ["Nest2:Foo"] = "Two",
                ["Nest2:Baz"] = "2",
            };

            ContextAwareObjectVisitor.Visit(@obj).Should().Equal(expected);
        }

        [Fact]
        public void Simple_anonymous_object()
        {
            var anon = new { Foo = "bar", Baz = 123 };

            var expected = new Dictionary<string, string>()
            {
                ["Foo"] = "bar",
                ["Baz"] = "123",
            };

            ContextAwareObjectVisitor.Visit(anon).Should().Equal(expected);
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

            var expected = new Dictionary<string, string>()
            {
                ["Qux"] = "foobar",
                ["Nest1:Foo"] = "One",
                ["Nest1:Baz"] = "1",
                ["Nest2:Foo"] = "Two",
                ["Nest2:Baz"] = "2",
            };

            ContextAwareObjectVisitor.Visit(anon).Should().Equal(expected);
        }

        [Fact]
        public void Nested_array_property()
        {
            var anon = new
            {
                foo = new
                {
                    items = new string[] { "foo", "bar" }
                }
            };

            var expected = new Dictionary<string, string>()
            {
                ["foo:items:0"] = "foo",
                ["foo:items:1"] = "bar"
            };

            ContextAwareObjectVisitor.Visit(anon).Should().Equal(expected);
        }

        [Fact]
        public void Nested_list_property()
        {
            var anon = new
            {
                foo = new
                {
                    items = new List<string>() { "foo", "bar" }
                }
            };

            var expected = new Dictionary<string, string>()
            {
                ["foo:items:0"] = "foo",
                ["foo:items:1"] = "bar"
            };

            ContextAwareObjectVisitor.Visit(anon).Should().Equal(expected);
        }

        [Fact]
        public void Nullable_primitive_property_with_null()
        {
            var @obj = new HasNullable()
            {
                NullableFoo = null
            };

            ContextAwareObjectVisitor.Visit(@obj).Should().BeEmpty();
        }

        [Fact]
        public void Nullable_primitive_property_with_value()
        {
            var @obj = new HasNullable()
            {
                NullableFoo = 123
            };

            var expected = new Dictionary<string, string>()
            {
                ["NullableFoo"] = "123",
            };

            ContextAwareObjectVisitor.Visit(@obj).Should().Equal(expected);
        }

        [Fact]
        public void String_property_with_null()
        {
            var @obj = new SimpleTestClass()
            {
                Foo = null,
                Baz = 123
            };

            var expected = new Dictionary<string, string>()
            {
                ["Baz"] = "123",
            };

            ContextAwareObjectVisitor.Visit(@obj).Should().Equal(expected);
        }

        [Fact]
        public void Dictionary_property()
        {
            var anon = new
            {
                items = new Dictionary<string, object>()
                {
                    ["foo"] = "bar",
                    ["baz"] = new { id = 1, qux = "blah" }
                }
            };

            var expected = new Dictionary<string, string>()
            {
                ["items:foo"] = "bar",
                ["items:baz:id"] = "1",
                ["items:baz:qux"] = "blah"
            };

            ContextAwareObjectVisitor.Visit(anon).Should().Equal(expected);
        }

        [Fact]
        public void Inherited_properties()
        {
            var @obj = new ExtendedTestClass()
            {
                Foo = "bar",
                Baz = 123,
                Qux = "abc"
            };

            var expected = new Dictionary<string, string>()
            {
                ["Qux"] = "abc",
                ["Foo"] = "bar",
                ["Baz"] = "123",
            };

            ContextAwareObjectVisitor.Visit(@obj).Should().Equal(expected);
        }
    }

    public class SimpleTestClass
    {
        public string Foo { get; set; }

        public int Baz { get; set; }
    }

    public class ExtendedTestClass : SimpleTestClass
    {
        public string Qux { get; set; }
    }

    public class NestedClass
    {
        public string Qux { get; set; }

        public SimpleTestClass Nest1 { get; set; }

        public SimpleTestClass Nest2 { get; set; }
    }

    public class HasNullable
    {
        public int? NullableFoo { get; set; }
    }
}
