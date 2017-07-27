// <copyright file="Default_tests.cs" company="Stormpath, Inc.">
// Copyright (c) 2016 Stormpath, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Stormpath.Configuration.Abstractions;
using Xunit;

namespace Stormpath.Configuration.Test
{
    [Collection("I/O")]
    public class Modified_config_tests
    {
        public static IEnumerable<object[]> FileTestCases()
        {
            yield return new object[] { new ModifiedConfigTestCases.YamlTestCase() };
            yield return new object[] { new ModifiedConfigTestCases.JsonTestCase() };
        }

        public Modified_config_tests()
        {
            Cleanup();
        }

        private static void Cleanup()
        {
            // Clean up any stray items
            foreach (var entry in FileTestCases())
            {
                var testCase = entry[0] as ConfigTestCaseBase;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), testCase.Filename);

                File.Delete(filePath);
            }
        }

        [Theory]
        [MemberData(nameof(FileTestCases))]
        public void Loading_modified_config(ConfigTestCaseBase testCase)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), testCase.Filename);

            File.WriteAllText(filePath, testCase.FileContents);

            var config = ConfigurationLoader.Initialize().Load();

            ValidateConfig(config);

            Cleanup();
        }

        [Fact]
        public void Supplied_by_immutable_instance()
        {
            var webConfiguration = new Abstractions.Immutable.WebConfiguration(
                serverUri: "https://localhost:9000",
                basePath: "#/",

                oauth2Route: new Abstractions.Immutable.WebOauth2RouteConfiguration(
                    enabled: false,
                    uri: "/oauth2/token",
                    clientCredentialsGrant: new Abstractions.Immutable.WebOauth2ClientCredentialsGrantConfiguration(
                        enabled: false,
                        accessToken: new Abstractions.Immutable.WebOauth2TokenConfiguration(timeToLive: 3601)
                    ),
                    passwordGrant: new Abstractions.Immutable.WebOauth2PasswordGrantConfiguration(
                        enabled: false,
                        validationStrategy: WebOauth2TokenValidationStrategy.Stormpath)
                ),

                accessTokenCookie: new Abstractions.Immutable.WebCookieConfiguration(
                    name: "accessToken",
                    httpOnly: false,
                    secure: false,
                    path: "/bar",
                    domain: "foo.bar"),

                refreshTokenCookie: new Abstractions.Immutable.WebRefreshTokenCookieConfiguration(
                    name: "refreshToken",
                    httpOnly: false,
                    secure: true,
                    path: "/foo",
                    domain: "baz.qux",
                    maxAge: 60000),

                produces: new List<string>()
                {
                    "foo/bar",
                },

                registerRoute: new Abstractions.Immutable.WebRegisterRouteConfiguration(
                    enabled: false,
                    uri: "/register1",
                    nextUri: "/1",
                    autoLogin: true,
                    emailVerificationRequired: true,
                    view: "registerView",
                    form: new Abstractions.Immutable.WebRegisterRouteFormConfiguration(
                        fields: new Dictionary<string, Abstractions.Immutable.WebFieldConfiguration>()
                        {
                            ["email"] = new Abstractions.Immutable.WebFieldConfiguration(
                                enabled: false,
                                visible: false,
                                label: "I Can Has Email",
                                placeholder: "Can Has?",
                                required: false,
                                type: "text")
                        },
                        fieldOrder: new List<string>()
                        {
                            "email",
                            "hidden",
                        })
                    ),

                verifyRoute: new Abstractions.Immutable.WebVerifyEmailRouteConfiguration(
                    enabled: true,
                    uri: "/verify1",
                    nextUri: "/login2",
                    view: "verifyView"),

                loginRoute: new Abstractions.Immutable.WebLoginRouteConfiguration(
                    enabled: false,
                    uri: "/login3",
                    nextUri: "/3",
                    view: "loginView",
                    form: new Abstractions.Immutable.WebLoginRouteFormConfiguration(
                        fields: new Dictionary<string, Abstractions.Immutable.WebFieldConfiguration>()
                        {
                            ["password"] = new Abstractions.Immutable.WebFieldConfiguration(
                                enabled: false,
                                visible: false,
                                label: "Password?",
                                placeholder: "Maybe",
                                required: false,
                                type: "email")
                        },
                        fieldOrder: new List<string>()
                        {
                            "password",
                        })
                    ),

                logoutRoute: new Abstractions.Immutable.WebLogoutRouteConfiguration(
                    enabled: false,
                    uri: "/logout4",
                    nextUri: "/4"),

                forgotPasswordRoute: new Abstractions.Immutable.WebForgotPasswordRouteConfiguration(
                    enabled: true,
                    uri: "/forgot5",
                    view: "forgot-password-view",
                    nextUri: "/login?status=forgot!"),

                changePasswordRoute: new Abstractions.Immutable.WebChangePasswordRouteConfiguration(
                    enabled: true,
                    uri: "/change6",
                    nextUri: "/login?status=reset?",
                    view: "change-password-view",
                    errorUri: "/forgot?status=invalid_sptoken:("),

                callbackRoute: new Abstractions.Immutable.WebCallbackRouteConfiguration(
                    enabled: false,
                    uri: "/stormpath-callback"),

                social: new Dictionary<string, Abstractions.Immutable.WebSocialProviderConfiguration>()
                {
                    ["facebook"] = new Abstractions.Immutable.WebSocialProviderConfiguration("Bookface", "email birthday"),
                    ["microsoft"] = new Abstractions.Immutable.WebSocialProviderConfiguration("M$", "user:everything"),
                    ["google"] = new Abstractions.Immutable.WebSocialProviderConfiguration("Elgoog", "email profile friends"),
                    ["linkedin"] = new Abstractions.Immutable.WebSocialProviderConfiguration("Also M$", "email interests")
                },

                meRoute: new Abstractions.Immutable.WebMeRouteConfiguration(
                    expand: new Dictionary<string, bool>()
                    {
                        ["directory"] = true
                    },
                    enabled: false,
                    uri: "/myself")
            );

            var stormpathConfiguration = new Abstractions.Immutable.StormpathConfiguration(
                apiToken: "foobarApiToken",
                org: "https://dev-12345.oktapreview.com",
                application: new Abstractions.Immutable.OktaApplicationConfiguration(id: "LightsabersGalore.app.foo"),
                web: webConfiguration);

            var config = ConfigurationLoader.Initialize().Load(stormpathConfiguration);

            ValidateConfig(config);
        }

        [Fact]
        public void Supplied_by_mutable_instance()
        {
            var stormpathConfiguration = new StormpathConfiguration()
            {
                ApiToken = "foobarApiToken",
                Org = "https://dev-12345.oktapreview.com",
                Application = new OktaApplicationConfiguration
                {
                    Id = "LightsabersGalore.app.foo"
                },
                Web = new WebConfiguration()
                {
                    ServerUri = "https://localhost:9000",
                    BasePath = "#/",
                    Oauth2 = new WebOauth2RouteConfiguration()
                    {
                        Enabled = false,
                        Uri = "/oauth2/token",
                        Client_Credentials = new WebOauth2ClientCredentialsGrantConfiguration()
                        {
                            Enabled = false,
                            AccessToken = new WebOauth2TokenConfiguration() { Ttl = 3601 }
                        },
                        Password = new WebOauth2PasswordGrantConfiguration()
                        {
                            Enabled = false,
                            ValidationStrategy = WebOauth2TokenValidationStrategy.Stormpath
                        }
                    },
                    AccessTokenCookie = new WebCookieConfiguration()
                    {
                        Name = "accessToken",
                        HttpOnly = false,
                        Secure = false,
                        Path = "/bar",
                        Domain = "foo.bar"
                    },
                    RefreshTokenCookie = new WebRefreshTokenCookieConfiguration()
                    {
                        Name = "refreshToken",
                        HttpOnly = false,
                        Secure = true,
                        Path = "/foo",
                        Domain = "baz.qux",
                        MaxAge = 60000
                    },
                    Produces = new List<string>()
                    {
                        "foo/bar"
                    },
                    Register = new WebRegisterRouteConfiguration()
                    {
                        Enabled = false,
                        Uri = "/register1",
                        NextUri = "/1",
                        AutoLogin = true,
                        EmailVerificationRequired = true,
                        View = "registerView",
                        Form = new WebRegisterRouteFormConfiguration()
                        {
                            Fields = new Dictionary<string, WebFieldConfiguration>()
                            {
                                ["email"] = new WebFieldConfiguration()
                                {
                                    Enabled = false,
                                    Visible = false,
                                    Label = "I Can Has Email",
                                    Placeholder = "Can Has?",
                                    Required = false,
                                    Type = "text"
                                }
                            },
                            FieldOrder = new List<string>()
                            {
                                "email", "hidden"
                            }
                        }
                    },
                    VerifyEmail = new WebVerifyEmailRouteConfiguration()
                    {
                        Enabled = true,
                        Uri = "/verify1",
                        NextUri = "/login2",
                        View = "verifyView"
                    },
                    Login = new WebLoginRouteConfiguration()
                    {
                        Enabled = false,
                        Uri = "/login3",
                        NextUri = "/3",
                        View = "loginView",
                        Form = new WebLoginRouteFormConfiguration()
                        {
                            Fields = new Dictionary<string, WebFieldConfiguration>()
                            {
                                ["password"] = new WebFieldConfiguration()
                                {
                                    Enabled = false,
                                    Visible = false,
                                    Label = "Password?",
                                    Placeholder = "Maybe",
                                    Required = false,
                                    Type = "email"
                                }
                            },
                            FieldOrder = new List<string>()
                            {
                                "password"
                            }
                        }
                    },
                    Logout = new WebLogoutRouteConfiguration()
                    {
                        Enabled = false,
                        Uri = "/logout4",
                        NextUri = "/4"
                    },
                    ForgotPassword = new WebForgotPasswordRouteConfiguration()
                    {
                        Enabled = true,
                        Uri = "/forgot5",
                        View = "forgot-password-view",
                        NextUri = "/login?status=forgot!"
                    },
                    ChangePassword = new WebChangePasswordRouteConfiguration()
                    {
                        Enabled = true,
                        Uri = "/change6",
                        NextUri = "/login?status=reset?",
                        View = "change-password-view",
                        ErrorUri = "/forgot?status=invalid_sptoken:("
                    },
                    Callback = new WebCallbackRouteConfiguration()
                    {
                        Enabled = false,
                        Uri = "/stormpath-callback"
                    },
                    Social = new Dictionary<string, WebSocialProviderConfiguration>()
                    {
                        ["facebook"] = new WebSocialProviderConfiguration() { DisplayName = "Bookface", Scope = "email birthday" },
                        ["microsoft"] = new WebSocialProviderConfiguration() { DisplayName = "M$", Scope = "user:everything" },
                        ["google"] = new WebSocialProviderConfiguration() { DisplayName = "Elgoog", Scope = "email profile friends" },
                        ["linkedin"] = new WebSocialProviderConfiguration() { DisplayName = "Also M$", Scope = "email interests" }
                    },
                    Me = new WebMeRouteConfiguration()
                    {
                        Enabled = false,
                        Uri = "/myself",
                        Expand = new Dictionary<string, bool>()
                        {
                            ["directory"] = true
                        }
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(stormpathConfiguration);

            ValidateConfig(config);
        }

        [Fact]
        public void Supplied_by_anonymous_type()
        {
            var userConfiguration = new
            {
                apiToken = "foobarApiToken",
                org = "https://dev-12345.oktapreview.com",
                application = new { id = "LightsabersGalore.app.foo" },

                // These old properties should not cause an exception
                client = new
                {
                    apiKey = new
                    {
                        id = "modified-foobar",
                        secret = "modified-barbaz"
                    },

                    cacheManager = new
                    {
                        enabled = false,
                        defaultTtl = 500,
                        defaultTti = 600,
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

                    baseUrl = "https://api.foo.com/v1",
                    connectionTimeout = 90,

                    proxy = new
                    {
                        port = 8088,
                        host = "proxy.foo.bar",
                        username = "foo",
                        password = "bar",
                    }
                },

                web = new
                {
                    serverUri = "https://localhost:9000",
                    basePath = "#/",

                    oauth2 = new
                    {
                        enabled = false,
                        uri = "/oauth2/token",
                        client_credentials = new
                        {
                            enabled = false,
                            accessToken = new
                            {
                                ttl = 3601
                            }
                        },

                        password = new
                        {
                            enabled = false,
                            validationStrategy = WebOauth2TokenValidationStrategy.Stormpath
                        }
                    },

                    accessTokenCookie = new
                    {
                        name = "accessToken",
                        httpOnly = false,
                        secure = false,
                        path = "/bar",
                        domain = "foo.bar",
                    },

                    refreshTokenCookie = new
                    {
                        name = "refreshToken",
                        httpOnly = false,
                        secure = true,
                        path = "/foo",
                        domain = "baz.qux",
                        maxAge = 60000
                    },

                    produces = new string[] { "foo/bar" },

                    Register = new
                    {
                        enabled = false,
                        uri = "/register1",
                        nextUri = "/1",
                        autoLogin = true,
                        emailVerificationRequired = true,
                        view = "registerView",
                        form = new
                        {
                            fields = new
                            {
                                email = new
                                {
                                    enabled = false,
                                    label = "I Can Has Email",
                                    placeholder = "Can Has?",
                                    required = false,
                                    type = "text",
                                },
                            },
                            fieldOrder = new List<string>()
                            {
                                "email",
                                "hidden",
                            },
                        },
                    },

                    verifyEmail = new
                    {
                        enabled = true,
                        uri = "/verify1",
                        nextUri = "/login2",
                        view = "verifyView"
                    },

                    login = new
                    {
                        enabled = false,
                        uri = "/login3",
                        nextUri = "/3",
                        view = "loginView",
                        form = new
                        {
                            Fields = new
                            {
                                password = new
                                {
                                    enabled = false,
                                    label = "Password?",
                                    placeholder = "Maybe",
                                    required = false,
                                    type = "email",
                                },
                            },
                            fieldOrder = new List<string>()
                            {
                                "password",
                            }
                        }
                    },

                    logout = new
                    {
                        Enabled = false,
                        Uri = "/logout4",
                        NextUri = "/4"
                    },

                    ForgotPassword = new
                    {
                        enabled = true,
                        uri = "/forgot5",
                        view = "forgot-password-view",
                        nextUri = "/login?status=forgot!",
                    },

                    changePassword = new
                    {
                        enabled = true,
                        uri = "/change6",
                        nextUri = "/login?status=reset?",
                        view = "change-password-view",
                        errorUri = "/forgot?status=invalid_sptoken:(",
                    },

                    idSite = new
                    {
                        enabled = true,
                        uri = "/idSiteResultz",
                        nextUri = "/123",
                        loginUri = "/456",
                        forgotUri = "/#/forgot789",
                        registerUri = "/#/register0"
                    },

                    callback = new
                    {
                        enabled = false,
                        uri = "/stormpath-callback"
                    },

                    social = new
                    {
                        facebook = new
                        {
                            displayName = "Bookface",
                            scope = "email birthday"
                        },
                        microsoft = new
                        {
                            displayname = "M$",
                            scope = "user:everything"
                        },
                        google = new
                        {
                            DisplayName = "Elgoog",
                            scope = "email profile friends"
                        },
                        linkedin = new
                        {
                            displayName = "Also M$",
                            scope = "email interests"
                        },
                    },

                    me = new
                    {
                        enabled = false,
                        uri = "/myself",
                        expand = new
                        {
                            directory = true
                        }
                    }
                }
            };

            var config = ConfigurationLoader.Initialize().Load(userConfiguration);

            ValidateConfig(config);
        }

        private static void ValidateConfig(Abstractions.Immutable.StormpathConfiguration config)
        {
            // Okta section
            config.ApiToken.Should().Be("foobarApiToken");
            config.Org.Should().Be("https://dev-12345.oktapreview.com");
            config.Application.Id.Should().Be("LightsabersGalore.app.foo");

            // Web section
            config.Web.ServerUri.Should().Be("https://localhost:9000");
            config.Web.BasePath.Should().Be("#/");

            config.Web.Oauth2.Enabled.Should().BeFalse();
            config.Web.Oauth2.Uri.Should().Be("/oauth2/token");
            config.Web.Oauth2.Client_Credentials.Enabled.Should().BeFalse();
            config.Web.Oauth2.Client_Credentials.AccessToken.Ttl.Should().Be(3601);
            config.Web.Oauth2.Password.Enabled.Should().BeFalse();
            config.Web.Oauth2.Password.ValidationStrategy.Should().Be(WebOauth2TokenValidationStrategy.Stormpath);

            config.Web.AccessTokenCookie.Name.Should().Be("accessToken");
            config.Web.AccessTokenCookie.HttpOnly.Should().BeFalse();
            config.Web.AccessTokenCookie.Secure.Should().BeFalse();
            config.Web.AccessTokenCookie.Path.Should().Be("/bar");
            config.Web.AccessTokenCookie.Domain.Should().Be("foo.bar");

            config.Web.RefreshTokenCookie.Name.Should().Be("refreshToken");
            config.Web.RefreshTokenCookie.HttpOnly.Should().BeFalse();
            config.Web.RefreshTokenCookie.Secure.Should().BeTrue();
            config.Web.RefreshTokenCookie.Path.Should().Be("/foo");
            config.Web.RefreshTokenCookie.Domain.Should().Be("baz.qux");
            config.Web.RefreshTokenCookie.MaxAge.Should().Be(60000);

            config.Web.Produces.ShouldBeEquivalentTo(
                new List<string>()
            {
                "foo/bar",
            },
                opt => opt.WithStrictOrdering()
            );

            config.Web.Register.Enabled.Should().BeFalse();
            config.Web.Register.Uri.Should().Be("/register1");
            config.Web.Register.NextUri.Should().Be("/1");
            config.Web.Register.AutoLogin.Should().BeTrue();
            config.Web.Register.EmailVerificationRequired.Should().BeTrue();
            config.Web.Register.View.Should().Be("registerView");
            config.Web.Register.Form.Fields.Should().HaveCount(1);

            config.Web.Register.Form.Fields["email"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["email"].Visible.Should().BeFalse();
            config.Web.Register.Form.Fields["email"].Label.Should().Be("I Can Has Email");
            config.Web.Register.Form.Fields["email"].Placeholder.Should().Be("Can Has?");
            config.Web.Register.Form.Fields["email"].Required.Should().BeFalse();
            config.Web.Register.Form.Fields["email"].Type.Should().Be("text");

            config.Web.Register.Form.FieldOrder.ShouldBeEquivalentTo(new List<string>()
            {
                "email",
                "hidden",
            },
                opt => opt.WithStrictOrdering()
            );

            config.Web.VerifyEmail.Enabled.Should().BeTrue();
            config.Web.VerifyEmail.Uri.Should().Be("/verify1");
            config.Web.VerifyEmail.NextUri.Should().Be("/login2");
            config.Web.VerifyEmail.View.Should().Be("verifyView");

            config.Web.Login.Enabled.Should().BeFalse();
            config.Web.Login.Uri.Should().Be("/login3");
            config.Web.Login.NextUri.Should().Be("/3");
            config.Web.Login.View.Should().Be("loginView");

            config.Web.Login.Form.Fields["password"].Enabled.Should().BeFalse();
            config.Web.Login.Form.Fields["password"].Visible.Should().BeFalse();
            config.Web.Login.Form.Fields["password"].Label.Should().Be("Password?");
            config.Web.Login.Form.Fields["password"].Placeholder.Should().Be("Maybe");
            config.Web.Login.Form.Fields["password"].Required.Should().BeFalse();
            config.Web.Login.Form.Fields["password"].Type.Should().Be("email");
            config.Web.Login.Form.Fields.Should().HaveCount(1);

            config.Web.Login.Form.FieldOrder.ShouldBeEquivalentTo(new List<string>()
            {
                "password"
            },
                opt => opt.WithStrictOrdering()
            );

            config.Web.Logout.Enabled.Should().BeFalse();
            config.Web.Logout.Uri.Should().Be("/logout4");
            config.Web.Logout.NextUri.Should().Be("/4");

            config.Web.ForgotPassword.Enabled.Should().BeTrue();
            config.Web.ForgotPassword.Uri.Should().Be("/forgot5");
            config.Web.ForgotPassword.NextUri.Should().Be("/login?status=forgot!");
            config.Web.ForgotPassword.View.Should().Be("forgot-password-view");

            config.Web.ChangePassword.Enabled.Should().BeTrue();
            config.Web.ChangePassword.Uri.Should().Be("/change6");
            config.Web.ChangePassword.NextUri.Should().Be("/login?status=reset?");
            config.Web.ChangePassword.View.Should().Be("change-password-view");
            config.Web.ChangePassword.ErrorUri.Should().Be("/forgot?status=invalid_sptoken:(");

            config.Web.Callback.Enabled.Should().BeFalse();
            config.Web.Callback.Uri.Should().Be("/stormpath-callback");

            config.Web.Social["facebook"].DisplayName.Should().Be("Bookface");
            config.Web.Social["facebook"].Scope.Should().Be("email birthday");
            config.Web.Social["microsoft"].DisplayName.Should().Be("M$");
            config.Web.Social["microsoft"].Scope.Should().Be("user:everything");
            config.Web.Social["google"].DisplayName.Should().Be("Elgoog");
            config.Web.Social["google"].Scope.Should().Be("email profile friends");
            config.Web.Social["linkedin"].DisplayName.Should().Be("Also M$");
            config.Web.Social["linkedin"].Scope.Should().Be("email interests");

            config.Web.Me.Enabled.Should().BeFalse();
            config.Web.Me.Uri.Should().Be("/myself");

            config.Web.Me.Expand.Should().HaveCount(1);
            config.Web.Me.Expand["directory"].Should().BeTrue();
        }
    }
}
