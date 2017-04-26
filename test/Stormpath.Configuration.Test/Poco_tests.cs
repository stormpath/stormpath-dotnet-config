using FluentAssertions;
using Stormpath.Configuration.Abstractions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Poco_tests
    {
        [Fact]
        public void Default_dictionaries_are_case_insensitive()
        {
            Default.Configuration.Web.Social["facebook"].DisplayName.Should().Be("Facebook");
            Default.Configuration.Web.Social["Facebook"].DisplayName.Should().Be("Facebook");
        }

        [Fact]
        public void Copied_default_dictionaries_are_case_insensitive()
        {
            var config = Default.Configuration.DeepClone();

            config.Web.Social["facebook"].DisplayName.Should().Be("Facebook");
            config.Web.Social["Facebook"].DisplayName.Should().Be("Facebook");
        }

        [Fact]
        public void Default_is_immutable()
        {
            Default.Configuration.ApiToken.Should().BeNull();

            var modifiedConfig = ConfigurationLoader.Initialize().Load(new { apiToken = "foobar" });
            modifiedConfig.ApiToken.Should().Be("foobar");

            Default.Configuration.ApiToken.Should().BeNull();

            var originalConfig = ConfigurationLoader.Initialize().Load();
            originalConfig.ApiToken.Should().BeNull();
        }

        [Fact]
        public void Default_is_immutable_for_nested_properties()
        {
            Default.Configuration.Application.Id.Should().BeNull();

            var modifiedConfig = ConfigurationLoader.Initialize().Load(new { application = new { id = "foobar" } });
            modifiedConfig.Application.Id.Should().Be("foobar");

            Default.Configuration.Application.Id.Should().BeNull();

            var originalConfig = ConfigurationLoader.Initialize().Load();
            originalConfig.Application.Id.Should().BeNull();
        }

        [Fact]
        public void Default_is_immutable_for_nested_dictionaries()
        {
            Default.Configuration.Web.Social["Facebook"].DisplayName.Should().Be("Facebook");

            var config = ConfigurationLoader.Initialize().Load(new
            {
                web = new
                {
                    social = new
                    {
                        facebook = new
                        {
                            displayName = "FB"
                        }
                    }
                }
            });
            config.Web.Social["Facebook"].DisplayName.Should().Be("FB");

            Default.Configuration.Web.Social["Facebook"].DisplayName.Should().Be("Facebook");

            var originalConfig = ConfigurationLoader.Initialize().Load();
            originalConfig.Web.Social["Facebook"].DisplayName.Should().Be("Facebook");
        }
    }
}
