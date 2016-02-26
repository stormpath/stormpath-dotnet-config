// <copyright file="DefaultYamlConfigTestCase.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Test.DefaultConfigTestCases
{
    public class YamlTestCase : ConfigTestCaseBase
    {
        public override string TestName => "Yaml";

        public override string Filename => "stormpath.yaml";

        public override string FileContents
        {
            get
            {
                // Note: Providing values for client.apiKey.id and client.apiKey.secret
                // because validation will fail without them.

                return @"
---
client:
  apiKey:
    file: null
    id: ""default-foobar""
    secret: ""default-secret123!""
  cacheManager:
    defaultTtl: 300
    defaultTti: 300
    caches: {}
  baseUrl: ""https://api.stormpath.com/v1""
  connectionTimeout: 30000
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

  social:
    facebook:
      uri: ""/callbacks/facebook""
      scope: ""email""
    github:
      uri: ""/callbacks/github""
      scope: ""user:email""
    google:
      uri: ""/callbacks/google""
      scope: ""email profile""
    linkedin:
      uri: ""/callbacks/linkedin""
      scope: ""r_basicprofile, r_emailaddress""

  # The /me route is for front-end applications, it returns a JSON object with
  # the current user object
  me:
    enabled: true
    uri: ""/me""
    expand:
        apiKeys: false
        applications: false
        customData: false
        directory: false
        groupMemberships: false
        groups: false
        providerData: false
        tenant: false

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
            }
        }
    }
}
