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

            var poco = new StormpathConfiguration();

            var config = new ConfigurationBuilder()
                //.SetBasePath("..\\")
                //.AddJsonFile("defaultConfig.json")
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["application:name"] = "Foobar"
                })
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["application:namz"] = "Lightsabers Galore!"
                })
                .Build();

            poco.Application.Name = config.Get("application:name", Default.Configuration.Application.Name);
            poco.Client.CacheManager.DefaultTtl = config.Get<int?>("client:cacheManager:defaultTtl");
            poco.Client.CacheManager.Caches = config.Get<Dictionary<string, ClientCacheConfiguration>>("client:cacheManager:caches");

            poco.Application.Name.Should().Be("Lightsabers Galore!");
        }
    }
}
