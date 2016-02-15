﻿// <copyright file="Default_tests.cs" company="Stormpath, Inc.">
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
using Xunit;

namespace Stormpath.Configuration.Test
{
    public class Default_config_tests
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] { new DefaultConfigTestCases.YamlTestCase() };
            yield return new object[] { new DefaultConfigTestCases.JsonTestCase() };
        }

        public Default_config_tests()
        {
            Cleanup();
        }

        private static void Cleanup()
        {
            // Clean up any stray items
            foreach (var entry in TestCases())
            {
                var testCase = entry[0] as ConfigTestCaseBase;
                File.Delete(testCase.Filename);
            }
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Loading_Stormpath_defaults(ConfigTestCaseBase testCase)
        {
            File.WriteAllText(testCase.Filename, testCase.FileContents);

            var config = StormpathConfiguration.Load();

            // Client section
            config.Client.ApiKey.File.Should().BeNullOrEmpty();
            config.Client.ApiKey.Id.Should().Be("foobar");
            config.Client.ApiKey.Secret.Should().Be("secret123!");

            config.Client.CacheManager.DefaultTtl.Should().Be(300);
            config.Client.CacheManager.DefaultTti.Should().Be(300);

            config.Client.CacheManager.Caches.Should().HaveCount(0);

            config.Client.BaseUrl.Should().Be("https://api.stormpath.com/v1");
            config.Client.ConnectionTimeout.Should().Be(30);
            config.Client.AuthenticationScheme.Should().Be(Model.ClientAuthenticationScheme.SAuthc1);

            config.Client.Proxy.Port.Should().Be(null);
            config.Client.Proxy.Host.Should().BeNullOrEmpty();
            config.Client.Proxy.Username.Should().BeNullOrEmpty();
            config.Client.Proxy.Password.Should().BeNullOrEmpty();

            // Application section
            config.Application.Href.Should().BeNullOrEmpty();
            config.Application.Name.Should().BeNullOrEmpty();

            // Web section
            config.Web.BasePath.Should().BeNullOrEmpty();

            config.Web.Oauth2.Enabled.Should().BeTrue();
            config.Web.Oauth2.Uri.Should().Be("/oauth/token");
            config.Web.Oauth2.Client_Credentials.Enabled.Should().BeTrue();
            config.Web.Oauth2.Client_Credentials.AccessToken.Ttl.Should().Be(3600);
            config.Web.Oauth2.Password.Enabled.Should().BeTrue();
            config.Web.Oauth2.Password.ValidationStrategy.Should().Be(Model.WebOauth2TokenValidationStrategy.Local);

            config.Web.Expand.ShouldBeEquivalentTo(new Dictionary<string, bool>()
            {
                ["apiKeys"] = false,
                ["customData"] = true,
                ["directory"] = false,
                ["groups"] = false,
            },
                opt => opt.WithStrictOrdering()
            );

            config.Web.AccessTokenCookie.Name.Should().Be("access_token");
            config.Web.AccessTokenCookie.HttpOnly.Should().BeTrue();
            config.Web.AccessTokenCookie.Secure.Should().Be(null);
            config.Web.AccessTokenCookie.Path.Should().BeNullOrEmpty();
            config.Web.AccessTokenCookie.Domain.Should().BeNullOrEmpty();

            config.Web.RefreshTokenCookie.Name.Should().Be("refresh_token");
            config.Web.RefreshTokenCookie.HttpOnly.Should().BeTrue();
            config.Web.RefreshTokenCookie.Secure.Should().Be(null);
            config.Web.RefreshTokenCookie.Path.Should().BeNullOrEmpty();
            config.Web.RefreshTokenCookie.Domain.Should().BeNullOrEmpty();

            config.Web.Produces.ShouldBeEquivalentTo(
                new List<string>()
            {
                "text/html",
                "application/json",
            }, 
                opt => opt.WithStrictOrdering()
            );

            config.Web.Register.Enabled.Should().BeTrue();
            config.Web.Register.Uri.Should().Be("/register");
            config.Web.Register.NextUri.Should().Be("/");
            config.Web.Register.AutoLogin.Should().BeFalse();
            config.Web.Register.View.Should().Be("register");
            config.Web.Register.Form.Fields.Should().HaveCount(7);

            config.Web.Register.Form.Fields["givenName"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["givenName"].Label.Should().Be("First Name");
            config.Web.Register.Form.Fields["givenName"].Placeholder.Should().Be("First Name");
            config.Web.Register.Form.Fields["givenName"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["givenName"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["middleName"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["middleName"].Label.Should().Be("Middle Name");
            config.Web.Register.Form.Fields["middleName"].Placeholder.Should().Be("Middle Name");
            config.Web.Register.Form.Fields["middleName"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["middleName"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["surname"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["surname"].Label.Should().Be("Last Name");
            config.Web.Register.Form.Fields["surname"].Placeholder.Should().Be("Last Name");
            config.Web.Register.Form.Fields["surname"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["surname"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["username"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["username"].Label.Should().Be("Username");
            config.Web.Register.Form.Fields["username"].Placeholder.Should().Be("Username");
            config.Web.Register.Form.Fields["username"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["username"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["email"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["email"].Label.Should().Be("Email");
            config.Web.Register.Form.Fields["email"].Placeholder.Should().Be("Email");
            config.Web.Register.Form.Fields["email"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["email"].Type.Should().Be("email");

            config.Web.Register.Form.Fields["password"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["password"].Label.Should().Be("Password");
            config.Web.Register.Form.Fields["password"].Placeholder.Should().Be("Password");
            config.Web.Register.Form.Fields["password"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["password"].Type.Should().Be("password");

            config.Web.Register.Form.Fields["confirmPassword"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["confirmPassword"].Label.Should().Be("Confirm Password");
            config.Web.Register.Form.Fields["confirmPassword"].Placeholder.Should().Be("Confirm Password");
            config.Web.Register.Form.Fields["confirmPassword"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["confirmPassword"].Type.Should().Be("password");

            config.Web.Register.Form.FieldOrder.ShouldBeEquivalentTo(new List<string>()
            {
                "username",
                "givenName",
                "middleName",
                "surname",
                "email",
                "password",
                "confirmPassword"
            },
                opt => opt.WithStrictOrdering()
            );

            config.Web.VerifyEmail.Enabled.Should().Be(null);
            config.Web.VerifyEmail.Uri.Should().Be("/verify");
            config.Web.VerifyEmail.NextUri.Should().Be("/login");
            config.Web.VerifyEmail.View.Should().Be("verify");

            config.Web.Login.Enabled.Should().BeTrue();
            config.Web.Login.Uri.Should().Be("/login");
            config.Web.Login.NextUri.Should().Be("/");
            config.Web.Login.View.Should().Be("login");

            config.Web.Login.Form.Fields["login"].Enabled.Should().BeTrue();
            config.Web.Login.Form.Fields["login"].Label.Should().Be("Username or Email");
            config.Web.Login.Form.Fields["login"].Placeholder.Should().Be("Username or Email");
            config.Web.Login.Form.Fields["login"].Required.Should().BeTrue();
            config.Web.Login.Form.Fields["login"].Type.Should().Be("text");

            config.Web.Login.Form.Fields["password"].Enabled.Should().BeTrue();
            config.Web.Login.Form.Fields["password"].Label.Should().Be("Password");
            config.Web.Login.Form.Fields["password"].Placeholder.Should().Be("Password");
            config.Web.Login.Form.Fields["password"].Required.Should().BeTrue();
            config.Web.Login.Form.Fields["password"].Type.Should().Be("password");

            config.Web.Login.Form.FieldOrder.ShouldBeEquivalentTo(new List<string>()
            {
                "login",
                "password"
            },
                opt => opt.WithStrictOrdering()
            );

            config.Web.Logout.Enabled.Should().BeTrue();
            config.Web.Logout.Uri.Should().Be("/logout");
            config.Web.Logout.NextUri.Should().Be("/");

            config.Web.ForgotPassword.Enabled.Should().Be(null);
            config.Web.ForgotPassword.Uri.Should().Be("/forgot");
            config.Web.ForgotPassword.NextUri.Should().Be("/login?status=forgot");
            config.Web.ForgotPassword.View.Should().Be("forgot-password");

            config.Web.ChangePassword.Enabled.Should().Be(null);
            config.Web.ChangePassword.AutoLogin.Should().BeFalse();
            config.Web.ChangePassword.Uri.Should().Be("/change");
            config.Web.ChangePassword.NextUri.Should().Be("/login?status=reset");
            config.Web.ChangePassword.View.Should().Be("change-password");
            config.Web.ChangePassword.ErrorUri.Should().Be("/forgot?status=invalid_sptoken");

            config.Web.IdSite.Enabled.Should().BeFalse();
            config.Web.IdSite.Uri.Should().Be("/idSiteResult");
            config.Web.IdSite.NextUri.Should().Be("/");
            config.Web.IdSite.LoginUri.Should().BeNullOrEmpty();
            config.Web.IdSite.ForgotUri.Should().Be("/#/forgot");
            config.Web.IdSite.RegisterUri.Should().Be("/#/register");

            config.Web.SocialProviders.CallbackRoot.Should().Be("/callbacks");

            config.Web.Me.Enabled.Should().BeTrue();
            config.Web.Me.Uri.Should().Be("/me");

            config.Web.Spa.Enabled.Should().BeFalse();
            config.Web.Spa.View.Should().Be("index");

            config.Web.Unauthorized.View.Should().Be("unauthorized");

            Cleanup();
        }
    }
}