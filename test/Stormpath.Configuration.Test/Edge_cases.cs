﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Stormpath.Configuration.Abstractions;
using Stormpath.Configuration.Abstractions.Immutable;
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

            var config = ConfigurationLoader.Load(userConfiguration);

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
            var config = new StormpathConfiguration(Default.Configuration);

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

            var config = ConfigurationLoader.Load(userConfiguration);

            config.Web.Social["Facebook"].Uri.Should().Be("/fb/cb");
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void VerifyEmail_enabled_behaves_properly()
        {
            var verifyPasswordNull = new WebVerifyEmailRouteConfiguration(enabled: null);

            verifyPasswordNull.Enabled.Should().Be(null);

            var verifyPasswordEnabled = new WebVerifyEmailRouteConfiguration(enabled: true);

            verifyPasswordEnabled.Enabled.Should().BeTrue();
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void ForgotPassword_enabled_behaves_properly()
        {
            var forgotPasswordNull = new WebForgotPasswordRouteConfiguration(enabled: null);

            forgotPasswordNull.Enabled.Should().Be(null);

            var forgotPasswordEnabled = new WebForgotPasswordRouteConfiguration(enabled: true);

            forgotPasswordEnabled.Enabled.Should().BeTrue();
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void ChangePassword_enabled_behaves_properly()
        {
            var changePasswordNull = new WebChangePasswordRouteConfiguration(enabled: null);

            changePasswordNull.Enabled.Should().Be(null);

            var changePasswordEnabled = new WebChangePasswordRouteConfiguration(enabled: true);

            changePasswordEnabled.Enabled.Should().BeTrue();
        }
    }
}
