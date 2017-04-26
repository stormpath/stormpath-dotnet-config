using System.Collections.Generic;
using FluentAssertions;
using Stormpath.Configuration.Abstractions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Edge_cases
    {
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

            config.Web.BasePath.Should().Be("/");

            config.Web.Produces.ShouldBeEquivalentTo(new List<string>()
            {
                "foo/bar",
            });
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
                            displayName = "FB"
                        }
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Web.Social["Facebook"].DisplayName.Should().Be("FB");
        }

        /// <summary>
        /// Testing the default value behavior of these configuration nodes.
        /// Null should be an acceptable value and shouldn't be overwritten by another default value.
        /// </summary>
        [Fact]
        public void VerifyEmail_enabled_behaves_properly()
        {
            var verifyPasswordNull = new Abstractions.Immutable.WebVerifyEmailRouteConfiguration(enabled: null);

            verifyPasswordNull.Enabled.Should().BeFalse();

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

            changePasswordNull.Enabled.Should().BeFalse();

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
                ApiToken = "foobar",
                Application = new OktaApplicationConfiguration
                {
                    Id = "qux123"
                },
                Web = new WebConfiguration
                {
                    Login = new WebLoginRouteConfiguration
                    {
                        View = "test"
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(test);

            config.ApiToken.Should().Be("foobar");
            config.Application.Id.Should().Be("qux123");
            config.Web.Login.View.Should().Be("test");
        }

        [Fact]
        public void Default_cookie_paths_are_slash()
        {
            var dummyApiKeyConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(dummyApiKeyConfiguration);

            config.Web.AccessTokenCookie.Path.Should().Be("/");
            config.Web.RefreshTokenCookie.Path.Should().Be("/");
        }

        [Fact]
        public void Cookie_paths_follow_base_path_if_unspecified()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    }
                },
                web = new
                {
                    basePath = "/blah"
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Web.BasePath.Should().Be("/blah");

            config.Web.AccessTokenCookie.Path.Should().Be("/blah");
            config.Web.RefreshTokenCookie.Path.Should().Be("/blah");
        }

        [Fact]
        public void Explicit_cookie_paths_are_observed()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    }
                },
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
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Web.BasePath.Should().Be("/blah");

            config.Web.AccessTokenCookie.Path.Should().Be("/foo");
            config.Web.RefreshTokenCookie.Path.Should().Be("/bar");
        }

        [Fact]
        public void Server_uri_is_null_by_default()
        {
            var dummyApiKeyConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(dummyApiKeyConfiguration);

            config.Web.ServerUri.Should().BeNull();
        }

        [Fact]
        public void Server_uri_can_be_set()
        {
            var userConfiguration = new
            {
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    }
                },
                web = new
                {
                    serverUri = "http://localhost:9999"
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            config.Web.ServerUri.Should().Be("http://localhost:9999");
        }
    }
}
