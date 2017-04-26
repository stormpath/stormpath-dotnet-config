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
using Stormpath.Configuration.Abstractions.Immutable;
using Xunit;

namespace Stormpath.Configuration.Test
{
    [Collection("I/O")]
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
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), testCase.Filename);

                File.Delete(filePath);
            }
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Loading_Stormpath_defaults(ConfigTestCaseBase testCase)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), testCase.Filename);

            File.WriteAllText(filePath, testCase.FileContents);

            var config = ConfigurationLoader.Initialize().Load();

            ValidateConfig(config);

            Cleanup();
        }

        [Fact]
        public void Empty_configuration_loads_defaults()
        {
            var config = ConfigurationLoader.Initialize().Load(new
            {
                apiToken = "okta_apiToken",
                org = "okta_org",
                application = new
                {
                    id = "okta_application_id"
                }
            });

            ValidateConfig(config);
        }

        [Fact]
        public void Null_configuration_loads_defaults()
        {
            var config = ConfigurationLoader.Initialize().Load(new StormpathConfiguration(
                apiToken: "okta_apiToken",
                    org: "okta_org",
                    application: new OktaApplicationConfiguration(
                        id: "okta_application_id")));

            ValidateConfig(config);
        }

        private static void ValidateConfig(StormpathConfiguration config)
        {
            // Okta section
            config.ApiToken.Should().Be("okta_apiToken");
            config.Org.Should().Be("okta_org");
            config.Application.Id.Should().Be("okta_application_id");

            // Web section
            config.Web.ServerUri.Should().BeNullOrEmpty();
            config.Web.BasePath.Should().Be("/");

            config.Web.Oauth2.Enabled.Should().BeTrue();
            config.Web.Oauth2.Uri.Should().Be("/oauth/token");
            config.Web.Oauth2.Client_Credentials.Enabled.Should().BeTrue();
            config.Web.Oauth2.Client_Credentials.AccessToken.Ttl.Should().Be(3600);
            config.Web.Oauth2.Password.Enabled.Should().BeTrue();
            config.Web.Oauth2.Password.ValidationStrategy.Should().Be(Abstractions.WebOauth2TokenValidationStrategy.Local);

            config.Web.AccessTokenCookie.Name.Should().Be("access_token");
            config.Web.AccessTokenCookie.HttpOnly.Should().BeTrue();
            config.Web.AccessTokenCookie.Secure.Should().Be(null);
            config.Web.AccessTokenCookie.Path.Should().Be("/");
            config.Web.AccessTokenCookie.Domain.Should().BeNullOrEmpty();

            config.Web.RefreshTokenCookie.Name.Should().Be("refresh_token");
            config.Web.RefreshTokenCookie.HttpOnly.Should().BeTrue();
            config.Web.RefreshTokenCookie.Secure.Should().Be(null);
            config.Web.RefreshTokenCookie.Path.Should().Be("/");
            config.Web.RefreshTokenCookie.Domain.Should().BeNullOrEmpty();

            config.Web.Produces[0].Should().Be("application/json");
            config.Web.Produces[1].Should().Be("text/html");

            config.Web.Register.Enabled.Should().BeTrue();
            config.Web.Register.EmailVerificationRequired.Should().BeFalse();
            config.Web.Register.Uri.Should().Be("/register");
            config.Web.Register.NextUri.Should().Be("/");
            config.Web.Register.AutoLogin.Should().BeFalse();
            config.Web.Register.View.Should().Be("register");
            config.Web.Register.Form.Fields.Should().HaveCount(7);

            config.Web.Register.Form.Fields["givenName"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["givenName"].Visible.Should().BeTrue();
            config.Web.Register.Form.Fields["givenName"].Label.Should().Be("First Name");
            config.Web.Register.Form.Fields["givenName"].Placeholder.Should().Be("First Name");
            config.Web.Register.Form.Fields["givenName"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["givenName"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["middleName"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["middleName"].Visible.Should().BeTrue();
            config.Web.Register.Form.Fields["middleName"].Label.Should().Be("Middle Name");
            config.Web.Register.Form.Fields["middleName"].Placeholder.Should().Be("Middle Name");
            config.Web.Register.Form.Fields["middleName"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["middleName"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["surname"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["surname"].Visible.Should().BeTrue();
            config.Web.Register.Form.Fields["surname"].Label.Should().Be("Last Name");
            config.Web.Register.Form.Fields["surname"].Placeholder.Should().Be("Last Name");
            config.Web.Register.Form.Fields["surname"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["surname"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["username"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["username"].Visible.Should().BeTrue();
            config.Web.Register.Form.Fields["username"].Label.Should().Be("Username");
            config.Web.Register.Form.Fields["username"].Placeholder.Should().Be("Username");
            config.Web.Register.Form.Fields["username"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["username"].Type.Should().Be("text");

            config.Web.Register.Form.Fields["email"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["email"].Visible.Should().BeTrue();
            config.Web.Register.Form.Fields["email"].Label.Should().Be("Email");
            config.Web.Register.Form.Fields["email"].Placeholder.Should().Be("Email");
            config.Web.Register.Form.Fields["email"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["email"].Type.Should().Be("email");

            config.Web.Register.Form.Fields["password"].Enabled.Should().BeTrue();
            config.Web.Register.Form.Fields["password"].Visible.Should().BeTrue();
            config.Web.Register.Form.Fields["password"].Label.Should().Be("Password");
            config.Web.Register.Form.Fields["password"].Placeholder.Should().Be("Password");
            config.Web.Register.Form.Fields["password"].Required.Should().BeTrue();
            config.Web.Register.Form.Fields["password"].Type.Should().Be("password");

            config.Web.Register.Form.Fields["confirmPassword"].Enabled.Should().BeFalse();
            config.Web.Register.Form.Fields["confirmPassword"].Visible.Should().BeTrue();
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

            config.Web.VerifyEmail.Enabled.Should().BeFalse();
            config.Web.VerifyEmail.Uri.Should().Be("/verify");
            config.Web.VerifyEmail.NextUri.Should().Be("/login?status=verified");
            config.Web.VerifyEmail.View.Should().Be("verify");

            config.Web.Login.Enabled.Should().BeTrue();
            config.Web.Login.Uri.Should().Be("/login");
            config.Web.Login.NextUri.Should().Be("/");
            config.Web.Login.View.Should().Be("login");

            config.Web.Login.Form.Fields["login"].Enabled.Should().BeTrue();
            config.Web.Login.Form.Fields["login"].Visible.Should().BeTrue();
            config.Web.Login.Form.Fields["login"].Label.Should().Be("Username or Email");
            config.Web.Login.Form.Fields["login"].Placeholder.Should().Be("Username or Email");
            config.Web.Login.Form.Fields["login"].Required.Should().BeTrue();
            config.Web.Login.Form.Fields["login"].Type.Should().Be("text");

            config.Web.Login.Form.Fields["password"].Enabled.Should().BeTrue();
            config.Web.Login.Form.Fields["password"].Visible.Should().BeTrue();
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

            config.Web.ChangePassword.Enabled.Should().BeFalse();
            config.Web.ChangePassword.Uri.Should().Be("/change");
            config.Web.ChangePassword.NextUri.Should().Be("/login?status=reset");
            config.Web.ChangePassword.View.Should().Be("change-password");
            config.Web.ChangePassword.ErrorUri.Should().Be("/forgot?status=invalid_sptoken");

            config.Web.Callback.Enabled.Should().BeTrue();
            config.Web.Callback.Uri.Should().Be("/stormpathCallback");

            config.Web.Social["facebook"].DisplayName.Should().Be("Facebook");
            config.Web.Social["facebook"].Scope.Should().Be("openid");
            config.Web.Social["google"].DisplayName.Should().Be("Google");
            config.Web.Social["google"].Scope.Should().Be("openid");
            config.Web.Social["linkedin"].DisplayName.Should().Be("LinkedIn");
            config.Web.Social["linkedin"].Scope.Should().Be("openid");

            config.Web.Me.Enabled.Should().BeTrue();
            config.Web.Me.Uri.Should().Be("/me");

            config.Web.Me.Expand.Should().HaveCount(8);
            config.Web.Me.Expand["apiKeys"].Should().BeFalse();
            config.Web.Me.Expand["applications"].Should().BeFalse();
            config.Web.Me.Expand["customData"].Should().BeFalse();
            config.Web.Me.Expand["directory"].Should().BeFalse();
            config.Web.Me.Expand["groupMemberships"].Should().BeFalse();
            config.Web.Me.Expand["groups"].Should().BeFalse();
            config.Web.Me.Expand["providerData"].Should().BeFalse();
            config.Web.Me.Expand["tenant"].Should().BeFalse();
        }
    }
}
