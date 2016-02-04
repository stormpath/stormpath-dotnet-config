using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml.Test
{
    public class Parsing_tests
    {
        [Fact]
        public void Parsing_empty_file()
        {
            var contents = string.Empty;

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).Should().BeEmpty();
        }

        [Fact]
        public void Parsing_properties()
        {
            var contents = @"
---
foo: bar
baz: qux
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo"] = "bar",
                    ["baz"] = "qux",
                });
        }

        [Fact]
        public void Parsing_nested_properties()
        {
            var contents = @"
---
foo:
  bar: baz
  baz: qux
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar"] = "baz",
                    ["foo:baz"] = "qux"
                });
        }

        [Fact]
        public void Parsing_triple_nested_properties()
        {
            var contents = @"
---
foo:
  bar:
    baz: qux
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar:baz"] = "qux",
                });
        }

        [Fact]
        public void Parsing_multiple_nested_properties()
        {
            var contents = @"
---
foo:
  bar1: baz1
  bar2: baz2
zoo:
  baz1: qux1
  baz2: qux2
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar1"] = "baz1",
                    ["foo:bar2"] = "baz2",
                    ["zoo:baz1"] = "qux1",
                    ["zoo:baz2"] = "qux2"
                });
        }

        [Fact]
        public void Parsing_arrays()
        {
            var contents = @"
---
foo:
  - bar
  - baz
zoo:
  - lion
  - tiger
  - bear
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:0"] = "bar",
                    ["foo:1"] = "baz",
                    ["zoo:0"] = "lion",
                    ["zoo:1"] = "tiger",
                    ["zoo:2"] = "bear",
                });
        }

        [Fact]
        public void Parsing_arrays_and_properties()
        {
            var contents = @"
---
foo:
  - bar
  - baz
zoo:
  open: true
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:0"] = "bar",
                    ["foo:1"] = "baz",
                    ["zoo:open"] = "true",
                });
        }

        [Theory]
        [InlineData("!!null")]
        [InlineData("!!null blah")]
        [InlineData("Null")]
        [InlineData("NULL")]
        [InlineData("")]
        [InlineData("~")]
        public void Parsing_nulls(string @null)
        {
            var contents = $@"
---
foo: {@null}
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo"] = "",
                });
        }

        [Fact]
        public void Parsing_complex_property()
        {
            var contents = @"
---
foo:
  bar1: baz1
  bar2: baz2
  qux:
    - 123
    - 456
anotherFoo: 789
";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).ShouldBeEquivalentTo(
                new Dictionary<string, string>()
                {
                    ["foo:bar1"] = "baz1",
                    ["foo:bar2"] = "baz2",
                    ["foo:qux:0"] = "123",
                    ["foo:qux:1"] = "456",
                    ["anotherFoo"] = "789"
                });
        }

        [Fact(Skip = "todo")]
        public void Throws_FormatException_for_bad_YAML()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Move")]
        public void Parsing_Stormpath_defaults()
        {
            //todo remove and move to Configuration.Test

            var contents = @"
---
client:
  apiKey:
    file: null
    id: null
    secret: null
  cacheManager:
    defaultTtl: 300
    defaultTti: 300
    caches:
      account:
        ttl: 300
        tti: 300
  baseUrl: ""https://api.stormpath.com/v1""
  connectionTimeout: 30
  authenticationScheme: ""SAUTHC1""
  proxy:
    port: null
    host: null
    username: null
    password: null
application:
  name: null
  href: null
web:
  basePath: null

  oauth2:
    enabled: true
    uri: ""/oauth/token""
    client_credentials:
      enabled: true
      accessToken:
        ttl: 3600
    password:
      enabled: true
      validationStrategy: ""local""

  expand:
    apiKeys: false
    customData: true
    directory: false
    groups: false

  accessTokenCookie:
    name: ""access_token""
    httpOnly: true
    secure: null
    path: null
    domain: null

  refreshTokenCookie:
    name: ""refresh_token""
    httpOnly: true
    secure: null
    path: null
    domain: null

  # By default the Stormpath integration should respond to JSON and HTML
  # requests.  If either is removed from configuration, the integration should
  # not try to handle the response for the given content type.

  produces:
    - text/html
    - application/json

  register:
    enabled: true
    uri: ""/register""
    nextUri: ""/""
    # autoLogin is possible only if the email verification feature is disabled
    # on the default default account store of the defined Stormpath
    # application.
    autoLogin: false
    form:
      fields:
        givenName:
          enabled: true
          label: ""First Name""
          placeholder: ""First Name""
          required: true
          type: ""text""
        middleName:
          enabled: false
          label: ""Middle Name""
          placeholder: ""Middle Name""
          required: true
          type: ""text""
        surname:
          enabled: true
          label: ""Last Name""
          placeholder: ""Last Name""
          required: true
          type: ""text""
        username:
          enabled: false
          label: ""Username""
          placeholder: ""Username""
          required: true
          type: ""text""
        email:
          enabled: true
          label: ""Email""
          placeholder: ""Email""
          required: true
          type: ""email""
        password:
          enabled: true
          label: ""Password""
          placeholder: ""Password""
          required: true
          type: ""password""
        confirmPassword:
          enabled: false
          label: ""Confirm Password""
          placeholder: ""Confirm Password""
          required: true
          type: ""password""
      fieldOrder:
        - ""username""
        - ""givenName""
        - ""middleName""
        - ""surname""
        - ""email""
        - ""password""
        - ""confirmPassword""
    view: ""register""

  # Unless verifyEmail.enabled is specifically set to false, the email
  # verification feature must be automatically enabled if the default account
  # store for the defined Stormpath application has the email verification
  # workflow enabled.
  verifyEmail:
    enabled: null
    uri: ""/verify""
    nextUri: ""/login""
    view: ""verify""

  login:
    enabled: true
    uri: ""/login""
    nextUri: ""/""
    view: ""login""
    form:
      fields:
        login:
          enabled: true
          label: ""Username or Email""
          placeholder: ""Username or Email""
          required: true
          type: ""text""
        password:
          enabled: true
          label: ""Password""
          placeholder: ""Password""
          required: true
          type: ""password""
      fieldOrder:
        - ""login""
        - ""password""

  logout:
    enabled: true
    uri: ""/logout""
    nextUri: ""/""

  # Unless forgotPassword.enabled is explicitly set to false, this feature
  # will be automatically enabled if the default account store for the defined
  # Stormpath application has the password reset workflow enabled.
  forgotPassword:
    enabled: null
    uri: ""/forgot""
    view: ""forgot-password""
    nextUri: ""/login?status=forgot""

  # Unless changePassword.enabled is explicitly set to false, this feature
  # will be automatically enabled if the default account store for the defined
  # Stormpath application has the password reset workflow enabled.
  changePassword:
    enabled: null
    autoLogin: false
    uri: ""/change""
    nextUri: ""/login?status=reset""
    view: ""change-password""
    errorUri: ""/forgot?status=invalid_sptoken""

  # If idSite.enabled is true, the user should be redirected to ID site for
  # login, registration, and password reset.
  idSite:
    enabled: false
    uri: ""/idSiteResult""
    nextUri: ""/""
    loginUri: """"
    forgotUri: ""/#/forgot""
    registerUri: ""/#/register""

  socialProviders:
    callbackRoot: ""/callbacks""

  # The /me route is for front-end applications, it returns a JSON object with
  # the current user object
  me:
    enabled: true
    uri: ""/me""

  # If the developer wants our integration to serve their Single Page
  # Application (SPA) in response to HTML requests for our default routes,
  # such as /login, then they will need to enable this feature and tell us
  # where the root of their SPA is.  This is likely a file path on the
  # filesystem.
  #
  # If the developer does not want our integration to handle their SPA, they
  # will need to configure the framework themeslves and remove 'text/html'
  # from `stormpath.web.produces`, so that we don not serve our default
  # HTML views.
  spa:
    enabled: false
    view: index

  unauthorized:
    view: ""unauthorized""";

            var parser = new YamlConfigurationFileParser();

            parser.Parse(StreamFromString(contents)).Should().NotBeEmpty();
        }

        private static Stream StreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
