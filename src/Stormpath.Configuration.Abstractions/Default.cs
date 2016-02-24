using System;
using System.Collections.Generic;
using Stormpath.Configuration.Abstractions;
using Stormpath.Configuration.Abstractions.Model;

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Provides configuration defaults.
    /// </summary>
    public static class Default
    {
        /// <summary>
        /// Represents the default configuration defaults for a Stormpath client or integration.
        /// </summary>
        public static readonly StormpathConfiguration Configuration = new StormpathConfiguration()
        {
            // Sections from the Stormpath SDK Specification at
            // https://github.com/stormpath/stormpath-sdk-spec/blob/master/specifications/config.md
            Client = new ClientConfiguration()
            {
                ApiKey = new ClientApiKeyConfiguration()
                {
                    File = null,
                    Id = null,
                    Secret = null
                },

                CacheManager = new ClientCacheManagerConfiguration()
                {
                    DefaultTtl = 300,
                    DefaultTti = 300,
                    Caches = new Dictionary<string, ClientCacheConfiguration>(StringComparer.OrdinalIgnoreCase) { },
                },

                BaseUrl = "https://api.stormpath.com/v1",
                ConnectionTimeout = 30 * 1000,
                AuthenticationScheme = ClientAuthenticationScheme.SAuthc1,

                Proxy = new ClientProxyConfiguration()
                {
                    Port = null,
                    Host = null,
                    Username = null,
                    Password = null,
                }
            },
            Application = new ApplicationConfiguration()
            {
                Name = null,
                Href = null
            },

            // Section from the Stormpath Framework Specification at
            // https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md
            Web = new WebConfiguration()
            {
                BasePath = null,

                Oauth2 = new WebOauth2RouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/oauth/token",
                    Client_Credentials = new WebOauth2ClientCredentialsGrantConfiguration()
                    {
                        Enabled = true,
                        AccessToken = new WebOauth2TokenConfiguration()
                        {
                            Ttl = 3600
                        }
                    },

                    Password = new WebOauth2PasswordGrantConfiguration()
                    {
                        Enabled = true,
                        ValidationStrategy = WebOauth2TokenValidationStrategy.Local
                    }
                },

                Expand = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
                {
                    ["apiKeys"] = false,
                    ["customData"] = true,
                    ["directory"] = false,
                    ["groups"] = false
                },

                AccessTokenCookie = new WebCookieConfiguration()
                {
                    Name = "access_token",
                    HttpOnly = true,
                    Secure = null,
                    Path = null,
                    Domain = null,
                },

                RefreshTokenCookie = new WebCookieConfiguration()
                {
                    Name = "refresh_token",
                    HttpOnly = true,
                    Secure = null,
                    Path = null,
                    Domain = null,
                },

                Produces = new List<string>()
                {
                    "text/html",
                    "application/json",
                },

                Register = new WebRegisterRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/register",
                    NextUri = "/",
                    AutoLogin = false,
                    View = "register",
                    Form = new WebRegisterRouteFormConfiguration()
                    {
                        Fields = new Dictionary<string, WebFieldConfiguration>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["givenName"] = new WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "First Name",
                                Placeholder = "First Name",
                                Required = true,
                                Type = "text",
                            },
                            ["middleName"] = new WebFieldConfiguration()
                            {
                                Enabled = false,
                                Label = "Middle Name",
                                Placeholder = "Middle Name",
                                Required = true,
                                Type = "text",
                            },
                            ["surname"] = new WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Last Name",
                                Placeholder = "Last Name",
                                Required = true,
                                Type = "text",
                            },
                            ["username"] = new WebFieldConfiguration()
                            {
                                Enabled = false,
                                Label = "Username",
                                Placeholder = "Username",
                                Required = true,
                                Type = "text",
                            },
                            ["email"] = new WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Email",
                                Placeholder = "Email",
                                Required = true,
                                Type = "email",
                            },
                            ["password"] = new WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Password",
                                Placeholder = "Password",
                                Required = true,
                                Type = "password",
                            },
                            ["confirmPassword"] = new WebFieldConfiguration()
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

                VerifyEmail = new WebVerifyEmailRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/verify",
                    NextUri = "/login",
                    View = "verify"
                },

                Login = new WebLoginRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/login",
                    NextUri = "/",
                    View = "login",
                    Form = new WebLoginRouteFormConfiguration()
                    {
                        Fields = new Dictionary<string, WebFieldConfiguration>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["login"] = new WebFieldConfiguration()
                            {
                                Enabled = true,
                                Label = "Username or Email",
                                Placeholder = "Username or Email",
                                Required = true,
                                Type = "text",
                            },
                            ["password"] = new WebFieldConfiguration()
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

                Logout = new WebLogoutRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/logout",
                    NextUri = "/"
                },

                ForgotPassword = new WebForgotPasswordRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/forgot",
                    View = "forgot-password",
                    NextUri = "/login?status=forgot",
                },

                ChangePassword = new WebChangePasswordRouteConfiguration()
                {
                    Enabled = null,
                    AutoLogin = false,
                    Uri = "/change",
                    NextUri = "/login?status=reset",
                    View = "change-password",
                    ErrorUri = "/forgot?status=invalid_sptoken",
                },

                IdSite = new WebIdSiteRouteConfiguration()
                {
                    Enabled = false,
                    Uri = "/idSiteResult",
                    NextUri = "/",
                    LoginUri = "",
                    ForgotUri = "/#/forgot",
                    RegisterUri = "/#/register"
                },

                Social = new Dictionary<string, WebSocialProviderConfiguration>(StringComparer.OrdinalIgnoreCase)
                {
                    ["facebook"] = new WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/facebook",
                        Scope = "email"
                    },
                    ["github"] = new WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/github",
                        Scope = "user:email"
                    },
                    ["google"] = new WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/google",
                        Scope = "email profile"
                    },
                    ["linkedin"] = new WebSocialProviderConfiguration()
                    {
                        Uri = "/callbacks/linkedin"
                    },
                },

                Me = new WebMeRouteConfiguration()
                {
                    Expand = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["apiKeys"] = false,
                        ["customData"] = false,
                        ["directory"] = false,
                        ["groups"] = false,
                    },
                    Enabled = true,
                    Uri = "/me",
                },

                Spa = new WebSpaConfiguration()
                {
                    Enabled = false,
                    View = "index",
                },

                Unauthorized = new WebUnauthorizedConfiguration()
                {
                    View = "unauthorized",
                }
            }
        };
    }
}
