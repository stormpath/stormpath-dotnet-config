using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Stormpath.Configuration.Abstractions;
using Stormpath.Configuration.Abstractions.Model;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Edge_cases
    {
        [Fact]
        public void Modifying_cacheManager_defaults_but_not_cache_configurations()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },

                    cacheManager = new
                    {
                        defaultTtl = 500,
                        defaultTti = 600,
                    },
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            config.Client.CacheManager.DefaultTtl.Should().Be(500);
            config.Client.CacheManager.DefaultTti.Should().Be(600);

            // Since Caches was not set, it should have the default value (empty)
            config.Client.CacheManager.Caches.Should().HaveCount(0);
        }

        private static void ValidateCustomCacheConfiguration(StormpathConfiguration config)
        {
            config.Client.CacheManager.DefaultTtl.Should().Be(300); // the default
            config.Client.CacheManager.DefaultTti.Should().Be(300); // the default

            config.Client.CacheManager.Caches["application"].Ttl.Should().Be(450);
            config.Client.CacheManager.Caches["application"].Tti.Should().Be(700);
            config.Client.CacheManager.Caches["directory"].Ttl.Should().Be(200);
            config.Client.CacheManager.Caches["directory"].Tti.Should().Be(300);
        }

        [Fact]
        public void Modifying_cacheManager_cache_configurations_only()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },

                    cacheManager = new
                    {
                        caches = new
                        {
                            application = new
                            {
                                ttl = 450,
                                tti = 700
                            },
                            directory = new
                            {
                                ttl = 200,
                                tti = 300
                            }
                        },
                    },
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            ValidateCustomCacheConfiguration(config);
        }

        [Fact]
        public void Setting_cacheManager_caches_to_dictionary()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },

                    cacheManager = new
                    {
                        caches = new Dictionary<string, ClientCacheConfiguration>()
                        {
                            ["application"] = new ClientCacheConfiguration(timeToLive: 450, timeToIdle: 700),
                            ["directory"] = new ClientCacheConfiguration(timeToLive: 200, timeToIdle: 300),
                        }
                    },
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            ValidateCustomCacheConfiguration(config);
        }


        [Fact]
        public void Setting_cacheManager_caches_to_list()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },

                    cacheManager = new
                    {
                        caches = new List<KeyValuePair<string, ClientCacheConfiguration>>()
                        {
                            new KeyValuePair<string, ClientCacheConfiguration>("application", new ClientCacheConfiguration(timeToLive: 450, timeToIdle: 700)),
                            new KeyValuePair<string, ClientCacheConfiguration>("directory", new ClientCacheConfiguration(timeToLive: 200, timeToIdle: 300)),
                        }
                    },
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            ValidateCustomCacheConfiguration(config);
        }

        [Fact]
        public void Modifying_web_properties_but_not_expand()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },
                },

                web = new
                {
                    basePath = "foobar",
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            config.Web.BasePath.Should().Be("foobar");

            // Expand should still be defaults, because we didn't touch it!
            config.Web.Expand.ShouldBeEquivalentTo(new Dictionary<string, bool>()
            {
                ["apiKeys"] = false,
                ["customData"] = true,
                ["directory"] = false,
                ["groups"] = false,
            });
        }

        [Fact]
        public void Modifying_expand_only()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },
                },

                web = new
                {
                    expand = new Dictionary<string, bool>()
                    {
                        ["customData"] = true,
                        ["applications"] = true,
                    },
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            config.Web.BasePath.Should().Be(null); // the default

            config.Web.Expand.ShouldBeEquivalentTo(new Dictionary<string, bool>()
            {
                ["customData"] = true,
                ["applications"] = true,
            });
        }

        [Fact]
        public void Modifying_web_properties_but_not_produces()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },
                },

                web = new
                {
                    basePath = "foobar",
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            // Produces should still be the default, because we didn't touch it!
            config.Web.Produces.ShouldBeEquivalentTo(new List<string>()
            {
                "text/html",
                "application/json",
            });
        }

        public void Modifying_produces_only()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },
                },

                web = new
                {
                    produces = new List<string>()
                    {
                        "foo/bar",
                    },
                }
            };

            var config = ConfigurationLoader.Load(userConfiguration);

            config.Web.BasePath.Should().Be(null); // the default

            config.Web.Produces.ShouldBeEquivalentTo(new List<string>()
            {
                "foo/bar",
            });
        }
    }
}
