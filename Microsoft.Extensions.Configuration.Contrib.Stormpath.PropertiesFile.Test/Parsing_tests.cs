using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.PropertiesFile.Test
{
    public class Parsing_tests
    {
        [Fact]
        public void Parsing_empty_file()
        {
            var contents = string.Empty;

            var parser = new PropertiesConfigurationFileParser(root: null);

            parser.Parse(StreamFromString(contents)).Should().BeEmpty();
        }

        [Fact]
        public void Blank_lines_are_ignored()
        {
            var contents = @"


";
            var parser = new PropertiesConfigurationFileParser(root: null);

            parser.Parse(StreamFromString(contents)).Should().BeEmpty();
        }

        [Fact]
        public void Comments_are_ignored()
        {
            var contents = @"
! a comment.
# nope!
";
            var parser = new PropertiesConfigurationFileParser(root: null);

            parser.Parse(StreamFromString(contents)).Should().BeEmpty();
        }

        [Theory]
        [InlineData("foo=bar")]
        [InlineData("foo = bar")]
        [InlineData("  foo   =            bar   ")]
        public void Parsing_property(string contents)
        {
            var parser = new PropertiesConfigurationFileParser(root: null);

            parser.Parse(StreamFromString(contents)).Should().BeEquivalentTo(
                new KeyValuePair<string, string>("foo", "bar"));
        }

        [Fact]
        public void Adds_root()
        {
            var contents = @"bar = baz";
            var parser = new PropertiesConfigurationFileParser(root: "foo");

            parser.Parse(StreamFromString(contents)).Should().BeEquivalentTo(
                new KeyValuePair<string, string>("foo:bar", "baz"));
        }

        [Fact]
        public void Separators_are_parsed()
        {
            var contents = @"foo.bar = baz";
            var parser = new PropertiesConfigurationFileParser(root: null);

            parser.Parse(StreamFromString(contents)).Should().BeEquivalentTo(
                new KeyValuePair<string, string>("foo:bar", "baz"));
        }

        private static Stream StreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
