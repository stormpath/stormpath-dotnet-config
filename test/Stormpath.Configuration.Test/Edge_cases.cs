using System.Collections.Generic;
using FluentAssertions;
using Stormpath.Configuration.Abstractions;
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

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Client.CacheManager.DefaultTtl.Should().Be(500);
            config.Client.CacheManager.DefaultTti.Should().Be(600);

            // Since Caches was not set, it should have the default value (empty)
            config.Client.CacheManager.Caches.Should().HaveCount(0);
        }

        private static void ValidateCustomCacheConfiguration(Abstractions.Immutable.StormpathConfiguration config)
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

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

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
                        caches = new Dictionary<string, Abstractions.Immutable.ClientCacheConfiguration>()
                        {
                            ["application"] = new Abstractions.Immutable.ClientCacheConfiguration(timeToLive: 450, timeToIdle: 700),
                            ["directory"] = new Abstractions.Immutable.ClientCacheConfiguration(timeToLive: 200, timeToIdle: 300),
                        }
                    },
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

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
                        caches = new List<KeyValuePair<string, Abstractions.Immutable.ClientCacheConfiguration>>()
                        {
                            new KeyValuePair<string, Abstractions.Immutable.ClientCacheConfiguration>("application", new Abstractions.Immutable.ClientCacheConfiguration(timeToLive: 450, timeToIdle: 700)),
                            new KeyValuePair<string, Abstractions.Immutable.ClientCacheConfiguration>("directory", new Abstractions.Immutable.ClientCacheConfiguration(timeToLive: 200, timeToIdle: 300)),
                        }
                    },
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            ValidateCustomCacheConfiguration(config);
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

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            // Produces should still be the default, because we didn't touch it!
            config.Web.Produces.ShouldBeEquivalentTo(new List<string>()
            {
                "text/html",
                "application/json",
            });
        }

        [Fact]
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

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Web.BasePath.Should().Be(null); // the default

            config.Web.Produces.ShouldBeEquivalentTo(new List<string>()
            {
                "foo/bar",
            });
        }

        [Fact]
        public void Default_dictionaries_are_case_insensitive()
        {
            Default.Configuration.Web.Social["Facebook"].Uri.Should().Be("/callbacks/facebook");
        }

        [Fact]
        public void Copied_default_dictionaries_are_case_insensitive()
        {
            var config = new Abstractions.Immutable.StormpathConfiguration(Default.Configuration);

            config.Web.Social["Facebook"].Uri.Should().Be("/callbacks/facebook");
        }

        [Fact]
        public void Hydrated_dictionaries_are_case_insensitive()
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
                    social = new
                    {
                        facebook = new
                        {
                            uri = "/fb/cb"
                        }
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Web.Social["Facebook"].Uri.Should().Be("/fb/cb");
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void VerifyEmail_enabled_behaves_properly()
        {
            var verifyPasswordNull = new Abstractions.Immutable.WebVerifyEmailRouteConfiguration(enabled: null);

            verifyPasswordNull.Enabled.Should().Be(null);

            var verifyPasswordEnabled = new Abstractions.Immutable.WebVerifyEmailRouteConfiguration(enabled: true);

            verifyPasswordEnabled.Enabled.Should().BeTrue();
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void ForgotPassword_enabled_behaves_properly()
        {
            var forgotPasswordNull = new Abstractions.Immutable.WebForgotPasswordRouteConfiguration(enabled: null);

            forgotPasswordNull.Enabled.Should().Be(null);

            var forgotPasswordEnabled = new Abstractions.Immutable.WebForgotPasswordRouteConfiguration(enabled: true);

            forgotPasswordEnabled.Enabled.Should().BeTrue();
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void ChangePassword_enabled_behaves_properly()
        {
            var changePasswordNull = new Abstractions.Immutable.WebChangePasswordRouteConfiguration(enabled: null);

            changePasswordNull.Enabled.Should().Be(null);

            var changePasswordEnabled = new Abstractions.Immutable.WebChangePasswordRouteConfiguration(enabled: true);

            changePasswordEnabled.Enabled.Should().BeTrue();
        }

        /// <summary>
        /// Regression test for https://github.com/stormpath/stormpath-dotnet-config/issues/23
        /// </summary>
        [Fact]
        public void Mutable_config_initialization_works()
        {
            var test = new StormpathConfiguration
            {
                Application = new ApplicationConfiguration { Name = "My Application!" },
                Web = new WebConfiguration
                {
                    Login = new WebLoginRouteConfiguration
                    {
                        View = "test"
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(test);

            config.Application.Name.Should().Be("My Application!");
            config.Web.Login.View.Should().Be("test");
        }

        [Fact]
        public void Default_cookie_paths_are_slash()
        {
            var config = ConfigurationLoader.Initialize().Load();

            config.Web.AccessTokenCookie.Path.Should().Be("/");
            config.Web.RefreshTokenCookie.Path.Should().Be("/");
        }

        [Fact]
        public void Cookie_paths_follow_base_path_if_unspecified()
        {
            var config = ConfigurationLoader.Initialize().Load(new
            {
                web = new
                {
                    basePath = "/blah"
                }
            });

            config.Web.BasePath.Should().Be("/blah");

            config.Web.AccessTokenCookie.Path.Should().Be("/blah");
            config.Web.RefreshTokenCookie.Path.Should().Be("/blah");
        }

        [Fact]
        public void Explicit_cookie_paths_are_observed()
        {
            var config = ConfigurationLoader.Initialize().Load(new
            {
                web = new
                {
                    basePath = "/blah",
                    accessTokenCookie = new
                    {
                        path = "/foo"
                    },
                    refreshTokenCookie = new
                    {
                        path = "/bar"
                    }
                }
            });

            config.Web.BasePath.Should().Be("/blah");

            config.Web.AccessTokenCookie.Path.Should().Be("/foo");
            config.Web.RefreshTokenCookie.Path.Should().Be("/bar");
        }
    }
}
