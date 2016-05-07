using System;
using System.Collections.Generic;

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Provides Stormpath configuration defaults.
    /// </summary>
    public static class Default
    {
        /// <summary>
        /// Represents the default configuration defaults for a Stormpath client or integration.
        /// </summary>
        public static readonly Immutable.StormpathConfiguration Configuration = new Immutable.StormpathConfiguration()
        {
            // Sections from the Stormpath SDK Specification at
            // https://github.com/stormpath/stormpath-sdk-spec/blob/master/specifications/config.md
            Client = new Immutable.ClientConfiguration()
            {
                ApiKey = new Immutable.ClientApiKeyConfiguration()
                {
                    File = null,
                    Id = null,
                    Secret = null
                },

                CacheManager = new Immutable.ClientCacheManagerConfiguration()
                {
                    DefaultTtl = 300,
                    DefaultTti = 300,
                    Caches = new Dictionary<string, Immutable.ClientCacheConfiguration>(StringComparer.OrdinalIgnoreCase) { },
                },

                BaseUrl = "https://api.stormpath.com/v1",
                ConnectionTimeout = 30 * 1000,
                AuthenticationScheme = ClientAuthenticationScheme.SAuthc1,

                Proxy = new Immutable.ClientProxyConfiguration()
                {
                    Port = null,
                    Host = null,
                    Username = null,
                    Password = null,
                }
            },
            Application = new Immutable.ApplicationConfiguration()
            {
                Name = null,
                Href = null
            },

            // Section from the Stormpath Framework Specification at
            // https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md
            Web = new Immutable.WebConfiguration()
            {
                BasePath = null,

                Oauth2 = new Immutable.WebOauth2RouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/oauth/token",
                    Client_Credentials = new Immutable.WebOauth2ClientCredentialsGrantConfiguration()
                    {
                        Enabled = true,
                        AccessToken = new Immutable.WebOauth2TokenConfiguration()
                        {
                            Ttl = 3600
                        }
                    },

                    Password = new Immutable.WebOauth2PasswordGrantConfiguration()
                    {
                        Enabled = true,
                        ValidationStrategy = WebOauth2TokenValidationStrategy.Local
                    }
                },

                AccessTokenCookie = new Immutable.WebCookieConfiguration()
                {
                    Name = "access_token",
                    HttpOnly = true,
                    Secure = null,
                    Path = null,
                    Domain = null,
                },

                RefreshTokenCookie = new Immutable.WebCookieConfiguration()
                {
                    Name = "refresh_token",
                    HttpOnly = true,
                    Secure = null,
                    Path = null,
                    Domain = null,
                },

                Produces = new List<string>()
                {
                    "application/json",
                    "text/html"
                },

                Register = new Immutable.WebRegisterRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/register",
                    NextUri = "/",
                    AutoLogin = false,
                    View = "register",
                    Form = new Immutable.WebRegisterRouteFormConfiguration()
                    {
                        Fields = new Dictionary<string, Immutable.WebFieldConfiguration>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["givenName"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "First Name",
                                Placeholder = "First Name",
                                Required = true,
                                Type = "text",
                            },
                            ["middleName"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = false,
                                Label = "Middle Name",
                                Placeholder = "Middle Name",
                                Required = true,
                                Type = "text",
                            },
                            ["surname"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Last Name",
                                Placeholder = "Last Name",
                                Required = true,
                                Type = "text",
                            },
                            ["username"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = false,
                                Label = "Username",
                                Placeholder = "Username",
                                Required = true,
                                Type = "text",
                            },
                            ["email"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Email",
                                Placeholder = "Email",
                                Required = true,
                                Type = "email",
                            },
                            ["password"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Password",
                                Placeholder = "Password",
                                Required = true,
                                Type = "password",
                            },
                            ["confirmPassword"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = false,
                                Label = "Confirm Password",
                                Placeholder = "Confirm Password",
                                Required = true,
                                Type = "password",
                            }
                        },
                        FieldOrder = new List<string>()
                        {
                            "username",
                            "givenName",
                            "middleName",
                            "surname",
                            "email",
                            "password",
                            "confirmPassword",
                        },
                    },
                },

                VerifyEmail = new Immutable.WebVerifyEmailRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/verify",
                    NextUri = "/login?status=verified",
                    View = "verify"
                },

                Login = new Immutable.WebLoginRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/login",
                    NextUri = "/",
                    View = "login",
                    Form = new Immutable.WebLoginRouteFormConfiguration()
                    {
                        Fields = new Dictionary<string, Immutable.WebFieldConfiguration>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["login"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Username or Email",
                                Placeholder = "Username or Email",
                                Required = true,
                                Type = "text",
                            },
                            ["password"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Password",
                                Placeholder = "Password",
                                Required = true,
                                Type = "password",
                            },
                        },
                        FieldOrder = new List<string>()
                        {
                            "login",
                            "password",
                        }
                    }
                },

                Logout = new Immutable.WebLogoutRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/logout",
                    NextUri = "/"
                },

                ForgotPassword = new Immutable.WebForgotPasswordRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/forgot",
                    View = "forgot-password",
                    NextUri = "/login?status=forgot",
                },

                ChangePassword = new Immutable.WebChangePasswordRouteConfiguration()
                {
                    Enabled = null,
                    AutoLogin = false,
                    Uri = "/change",
                    NextUri = "/login?status=reset",
                    View = "change-password",
                    ErrorUri = "/forgot?status=invalid_sptoken",
                },

                IdSite = new Immutable.WebIdSiteConfiguration()
                {
                    Enabled = false,
                    LoginUri = "",
                    ForgotUri = "/#/forgot",
                    RegisterUri = "/#/register"
                },

                Callback = new Immutable.WebCallbackRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/stormpathCallback"
                },

                Social = new Dictionary<string, Immutable.WebSocialProviderConfiguration>(StringComparer.OrdinalIgnoreCase)
                {
                    ["facebook"] = new Immutable.WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/facebook",
                        Scope = "email"
                    },
                    ["github"] = new Immutable.WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/github",
                        Scope = "user:email"
                    },
                    ["google"] = new Immutable.WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/google",
                        Scope = "email profile"
                    },
                    ["linkedin"] = new Immutable.WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/linkedin",
                        Scope = "r_basicprofile, r_emailaddress"
                    },
                },

                Me = new Immutable.WebMeRouteConfiguration()
                {
                    Expand = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["apiKeys"] = false,
                        ["applications"] = false,
                        ["customData"] = false,
                        ["directory"] = false,
                        ["groupMemberships"] = false,
                        ["groups"] = false,
                        ["providerData"] = false,
                        ["tenant"] = false
                    },
                    Enabled = true,
                    Uri = "/me",
                }
            }
        };
    }
}
