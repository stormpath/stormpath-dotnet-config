using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class ConfigurationTest
    {
        public static readonly StormpathConfiguration DefaultConfiguration = new StormpathConfiguration()
        {
            Application = new ApplicationConfiguration()
            {
                Name = "Foobar",
                Href = null
            }
        };

        [Fact]
        public void MyTestMethod()
        {
            // Defaults
            //var poco = new StormpathConfiguration()
            //{
            //    Application = new ApplicationConfiguration()
            //    {
            //        Name = "Lightsabers Galore!"
            //    }
            //};

            var poco = new StormpathConfiguration();

            var config = new ConfigurationBuilder()
                //.SetBasePath("..\\")
                .AddJsonFile("defaultConfig.json")
                .Build();

            poco.Application.Name = config.Get("application:name", DefaultConfiguration.Application.Name);
            poco.Client.CacheManager.DefaultTtl = config.Get<int?>("client:cacheManager:defaultTtl");
            poco.Client.CacheManager.Caches = config.Get<Dictionary<string, ClientCacheConfiguration>>("client:cacheManager:caches");

            poco.Application.Name.Should().Be("Lightsabers Galore!");
        }
    }
}
