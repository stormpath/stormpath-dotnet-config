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
            Default.Configuration.Web.Social["facebook"].Uri.Should().Be("/callbacks/facebook");
            Default.Configuration.Web.Social["Facebook"].Uri.Should().Be("/callbacks/facebook");
        }

        [Fact]
        public void Copied_default_dictionaries_are_case_insensitive()
        {
            var config = new Abstractions.Immutable.StormpathConfiguration(Default.Configuration);

            config.Web.Social["facebook"].Uri.Should().Be("/callbacks/facebook");
            config.Web.Social["Facebook"].Uri.Should().Be("/callbacks/facebook");
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
    }
}
