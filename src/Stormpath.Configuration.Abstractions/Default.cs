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
            // SDK Client configuration
            // (https://github.com/stormpath/stormpath-sdk-spec/blob/master/specifications/config.md)
            Client = new Immutable.ClientConfiguration()
            {
                // The Stormpath credentials to use when connecting to the Stormpath API.
                ApiKey = new Immutable.ClientApiKeyConfiguration()
                {
                    File = null,
                    Id = null,
                    Secret = null
                },

                // Configures the SDK Client caching layer.
                // Uses an in-memory cache by default.
                CacheManager = new Immutable.ClientCacheManagerConfiguration()
                {
                    Enabled = true,
                    DefaultTtl = 3600,
                    DefaultTti = 3600,
                    Caches = new Dictionary<string, Immutable.ClientCacheConfiguration>(StringComparer.OrdinalIgnoreCase) { },
                },

                // The Stormpath Base URL to use.
                // You shouldn't have to modify this unless you are connecting to Stormpath Enterprise.
                BaseUrl = "https://api.stormpath.com/v1",

                // The default HTTP connection timeout, in seconds.
                ConnectionTimeout = 30,

                // The REST API authentication scheme to use.
                // SAuthc1  (the default) is more secure, but Basic is required in some hosting scenarios.
                AuthenticationScheme = ClientAuthenticationScheme.SAuthc1,

                // The HTTP proxy to use, if any.
                Proxy = new Immutable.ClientProxyConfiguration()
                {
                    Port = null,
                    Host = null,
                    Username = null,
                    Password = null,
                }
            },

            // Framework integration configuration
            // (https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md)
            // Either name or href is required to connect a front-end application to a Stormpath Application.
            // If left empty, the framework library will attempt to auto-resolve an Application in the current Stormpath Tenant.
            Application = new Immutable.ApplicationConfiguration()
            {
                Name = null,
                Href = null
            },

            // Framework integration configuration
            // (https://github.com/stormpath/stormpath-framework-spec/blob/master/configuration.md)
            Web = new Immutable.WebConfiguration()
            {
                // The base server URI (null unless it's specifically needed for social login integrations)
                ServerUri = null,

                // The base path is used as the default path for the cookies that are set.
                BasePath = "/",

                // The OAuth 2.0 route configuraiton.
                Oauth2 = new Immutable.WebOauth2RouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/oauth/token",

                    // The OAuth 2.0 client_credentials grant configuration.
                    Client_Credentials = new Immutable.WebOauth2ClientCredentialsGrantConfiguration()
                    {
                        Enabled = true,
                        AccessToken = new Immutable.WebOauth2TokenConfiguration()
                        {
                            Ttl = 3600
                        }
                    },

                    // The OAuth 2.0 password grant configuration.
                    Password = new Immutable.WebOauth2PasswordGrantConfiguration()
                    {
                        Enabled = true,

                        // Local validation checks the token signature.
                        // Stormpath (remote) validation makes a network request, but allows for token revocation.
                        ValidationStrategy = WebOauth2TokenValidationStrategy.Local
                    }
                },

                // The details of the Access Token cookie saved to the user's browser.
                AccessTokenCookie = new Immutable.WebCookieConfiguration()
                {
                    // Controls the name of the cookie.
                    Name = "access_token",

                    // Sets the HttpOnly cookie flag, to prevent XSS attacks from hijacking your cookies.
                    // We highly recommend that you do not change this.
                    HttpOnly = true,

                    // The secure property controls the Secure flag on the cookie.  The
                    // framework library will auto-detect if the incoming request is over HTTPS,
                    // by looking at the request protocol and turn on Secure if so.  You can
                    // override auto-detection and force the Secure flag on by setting this
                    // property to true, or force off by setting this property to false.
                    Secure = null,

                    // Controls the path flag of the cookie
                    // Inherits from basePath, but can be overridden here.
                    Path = null,

                    // Controls the domain flag on the cookie, will not be set unless specified.
                    Domain = "",
                },

                // The details of the Refresh Token cookie saved to the user's browser.
                // This has the same configuration options as the Access Token cookie (above).
                RefreshTokenCookie = new Immutable.WebCookieConfiguration()
                {
                    Name = "refresh_token",
                    HttpOnly = true,
                    Secure = null,
                    Path = null,
                    Domain = "",
                },

                // Controls what Content-Types the framework library can produce.
                // To disable JSON or HTML output, remove the corresponding item from this list.
                Produces = new List<string>()
                {
                    "application/json",
                    "text/html"
                },

                // The registration route configuration.
                Register = new Immutable.WebRegisterRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/register",

                    // Whether to automatically log in a user after registration.
                    AutoLogin = false,

                    // The URI to redirect to after successful registration, if AutoLogin is enabled.
                    NextUri = "/",

                    View = "register",

                    // The form or JSON view model to generate for this route.
                    Form = new Immutable.WebRegisterRouteFormConfiguration()
                    {
                        Fields = new Dictionary<string, Immutable.WebFieldConfiguration>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["givenName"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Visible = true,
                                Label = "First Name",
                                Placeholder = "First Name",
                                Required = true,
                                Type = "text",
                            },
                            ["middleName"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = false,
                                Visible = true,
                                Label = "Middle Name",
                                Placeholder = "Middle Name",
                                Required = true,
                                Type = "text",
                            },
                            ["surname"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Visible = true,
                                Label = "Last Name",
                                Placeholder = "Last Name",
                                Required = true,
                                Type = "text",
                            },
                            ["username"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = false,
                                Visible = true,
                                Label = "Username",
                                Placeholder = "Username",
                                Required = true,
                                Type = "text",
                            },
                            ["email"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Visible = true,
                                Label = "Email",
                                Placeholder = "Email",
                                Required = true,
                                Type = "email",
                            },
                            ["password"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Visible = true,
                                Label = "Password",
                                Placeholder = "Password",
                                Required = true,
                                Type = "password",
                            },
                            ["confirmPassword"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = false,
                                Visible = true,
                                Label = "Confirm Password",
                                Placeholder = "Confirm Password",
                                Required = true,
                                Type = "password",
                            }
                        },

                        // The order of the fields in the form or view model.
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

                // The email verification route configuration.
                VerifyEmail = new Immutable.WebVerifyEmailRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/verify",
                    NextUri = "/login?status=verified",
                    View = "verify"
                },

                // The login route configuration.
                Login = new Immutable.WebLoginRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/login",
                    NextUri = "/",
                    View = "login",
                    
                    // The form or JSON view model to generate for this route.
                    Form = new Immutable.WebLoginRouteFormConfiguration()
                    {
                        Fields = new Dictionary<string, Immutable.WebFieldConfiguration>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["login"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Visible = true,
                                Label = "Username or Email",
                                Placeholder = "Username or Email",
                                Required = true,
                                Type = "text",
                            },
                            ["password"] = new Immutable.WebFieldConfiguration()
                            {
                                Enabled = true,
                                Visible = true,
                                Label = "Password",
                                Placeholder = "Password",
                                Required = true,
                                Type = "password",
                            },
                        },

                        // The order of the fields in the form or view model.
                        FieldOrder = new List<string>()
                        {
                            "login",
                            "password",
                        }
                    }
                },

                // The logout route configuration.
                Logout = new Immutable.WebLogoutRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/logout",
                    NextUri = "/"
                },

                // The forgot password route configuration.
                ForgotPassword = new Immutable.WebForgotPasswordRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/forgot",
                    View = "forgot-password",
                    NextUri = "/login?status=forgot",
                },

                // The change password route configuration.
                ChangePassword = new Immutable.WebChangePasswordRouteConfiguration()
                {
                    Enabled = null,
                    Uri = "/change",
                    NextUri = "/login?status=reset",
                    View = "change-password",
                    ErrorUri = "/forgot?status=invalid_sptoken",
                },

                // Controls whether ID Site is used for registration, login, password reset, etc.
                IdSite = new Immutable.WebIdSiteConfiguration()
                {
                    Enabled = false,
                    LoginUri = "",
                    ForgotUri = "/#/forgot",
                    RegisterUri = "/#/register"
                },

                // The Stormpath callback route configuration.
                // This is used for ID Site and other Stormpath redirect flows.
                Callback = new Immutable.WebCallbackRouteConfiguration()
                {
                    Enabled = true,
                    Uri = "/stormpathCallback"
                },

                // The social login configuration.
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
                        Scope = "r_basicprofile r_emailaddress"
                    },
                },

                // The user details route configuration.
                Me = new Immutable.WebMeRouteConfiguration()
                {
                    // Whether to automatically expand and return linked Account properties.
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
