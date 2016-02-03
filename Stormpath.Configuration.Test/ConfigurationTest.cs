using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class ConfigurationTest
    {
        [Fact]
        public void MyTestMethod()
        {
            var config = StormpathConfiguration.Load();
        }
    }
}
