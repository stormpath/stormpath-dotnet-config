using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml.Test
{
    public class Parsing_tests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Parsing_empty_stream(string contents)
        {
            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).Should().BeEmpty();
        }

        [Fact]
        public void Parsing_properties()
        {
            var contents = @"
---
foo: bar
baz: qux
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo"] = "bar",
                    ["baz"] = "qux",
                });
        }

        [Fact]
        public void Parsing_nested_properties()
        {
            var contents = @"
---
foo:
  bar: baz
  baz: qux
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar"] = "baz",
                    ["foo:baz"] = "qux"
                });
        }

        [Fact]
        public void Parsing_triple_nested_properties()
        {
            var contents = @"
---
foo:
  bar:
    baz: qux
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar:baz"] = "qux",
                });
        }

        [Fact]
        public void Parsing_multiple_nested_properties()
        {
            var contents = @"
---
foo:
  bar1: baz1
  bar2: baz2
zoo:
  baz1: qux1
  baz2: qux2
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar1"] = "baz1",
                    ["foo:bar2"] = "baz2",
                    ["zoo:baz1"] = "qux1",
                    ["zoo:baz2"] = "qux2"
                });
        }

        [Fact]
        public void Parsing_arrays()
        {
            var contents = @"
---
foo:
  - bar
  - baz
zoo:
  - lion
  - tiger
  - bear
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:0"] = "bar",
                    ["foo:1"] = "baz",
                    ["zoo:0"] = "lion",
                    ["zoo:1"] = "tiger",
                    ["zoo:2"] = "bear",
                });
        }

        [Fact]
        public void Parsing_arrays_and_properties()
        {
            var contents = @"
---
foo:
  - bar
  - baz
zoo:
  open: true
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:0"] = "bar",
                    ["foo:1"] = "baz",
                    ["zoo:open"] = "true",
                });
        }

        [Fact]
        public void Parsing_arrays_of_properties()
        {
            var contents = @"
---
foo:
  - bar: baz
  - qux: zoo
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:0:bar"] = "baz",
                    ["foo:1:qux"] = "zoo",
                });
        }

        [Theory]
        [InlineData("!!null")]
        [InlineData("!!null blah")]
        [InlineData("Null")]
        [InlineData("NULL")]
        [InlineData("")]
        [InlineData("~")]
        public void Parsing_nulls(string @null)
        {
            var contents = $@"
---
foo: {@null}
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo"] = "",
                });
        }

        [Fact]
        public void Parsing_null_key()
        {
            var contents = @"
---
foo:
  ~:
    bar: baz
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo::bar"] = "baz",
                });
        }

        [Fact]
        public void Parsing_complex_property()
        {
            var contents = @"
---
foo:
  bar1: baz1
  bar2: baz2
  qux:
    - 123
    - 456
anotherFoo: 789
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar1"] = "baz1",
                    ["foo:bar2"] = "baz2",
                    ["foo:qux:0"] = "123",
                    ["foo:qux:1"] = "456",
                    ["anotherFoo"] = "789"
                });
        }

        [Fact]
        public void Throws_FormatException_for_bad_YAML()
        {
            var contents = @"
---
foo: bar: baz
";

            var parser = new YamlConfigurationFileParser();

            Action act = () => parser.Parse(StreamFromString(contents));

            act.ShouldThrow<FormatException>();
        }

        [Fact]
        public void Throws_FormatException_for_duplicate_key()
        {
            var contents = @"
---
foo: bar
foo: baz
";

            var parser = new YamlConfigurationFileParser();

            Action act = () => parser.Parse(StreamFromString(contents));

            act.ShouldThrow<FormatException>();
        }

        private static Stream StreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
