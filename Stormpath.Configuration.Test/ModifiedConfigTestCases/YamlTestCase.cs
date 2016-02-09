﻿// <copyright file="DefaultYamlConfigTestCase.cs" company="Stormpath, Inc.">
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

namespace Stormpath.Configuration.Test.ModifiedConfigTestCases
{
    public class YamlTestCase : ConfigTestCaseBase
    {
        public override string TestName => "Yaml";

        public override string Filename => "stormpath.yaml";

        public override string FileContents
        {
            get
            {
                return @"
---
client:
  apiKey:
    file: null
    id: foobar
    secret: barbaz
  cacheManager:
    defaultTtl: 500
    defaultTti: 600
    caches:
      application:
        ttl: 450
        tti: 700
      directory:
        ttl: 200
        tti: 300
  baseUrl: ""https://api.foo.com/v1""
  connectionTimeout: 90
  authenticationScheme: ""BASIC""
  proxy:
    port: 8088
    host: proxy.foo.bar
    username: foo
    password: bar
application:
  name: Lightsabers Galore
  href: ""https://api.foo.com/v1/apps/foo""
web:
  basePath: ""#/""

  oauth2:
    enabled: false
    uri: ""/oauth2/token""
    client_credentials:
      enabled: false
      accessToken:
        ttl: 3601
    password:
      enabled: false
      validationStrategy: ""remote""

  expand:
    customData: true
    applications: true

  accessTokenCookie:
    name: ""accessToken""
    httpOnly: false
    secure: false
    path: ""/""
    domain: ""foo.bar""

  refreshTokenCookie:
    name: ""refreshToken""
    httpOnly: false
    secure: true
    path: ""/foo""
    domain: ""baz.qux""

  # By default the Stormpath integration should respond to JSON and HTML
  # requests.  If either is removed from configuration, the integration should
  # not try to handle the response for the given content type.

  produces:
    - foo/bar

  register:
    enabled: false
    uri: ""/register1""
    nextUri: ""/1""
    # autoLogin is possible only if the email verification feature is disabled
    # on the default default account store of the defined Stormpath
    # application.
    autoLogin: true
    form:
      fields:
        email:
          enabled: false
          label: ""I Can Has Email""
          placeholder: ""Can Has?""
          required: false
          type: ""text""
      fieldOrder:
        - ""email""
        - ""hidden""
    view: ""registerView""

  # Unless verifyEmail.enabled is specifically set to false, the email
  # verification feature must be automatically enabled if the default account
  # store for the defined Stormpath application has the email verification
  # workflow enabled.
  verifyEmail:
    enabled: true
    uri: ""/verify1""
    nextUri: ""/login2""
    view: ""verifyView""

  login:
    enabled: false
    uri: ""/login3""
    nextUri: ""/3""
    view: ""loginView""
    form:
      fields:
        password:
          enabled: false
          label: ""Password?""
          placeholder: ""Maybe""
          required: false
          type: ""email""
      fieldOrder:
        - ""password""

  logout:
    enabled: false
    uri: ""/logout4""
    nextUri: ""/4""

  # Unless forgotPassword.enabled is explicitly set to false, this feature
  # will be automatically enabled if the default account store for the defined
  # Stormpath application has the password reset workflow enabled.
  forgotPassword:
    enabled: true
    uri: ""/forgot5""
    view: ""forgot-password-view""
    nextUri: ""/login?status=forgot!""

  # Unless changePassword.enabled is explicitly set to false, this feature
  # will be automatically enabled if the default account store for the defined
  # Stormpath application has the password reset workflow enabled.
  changePassword:
    enabled: true
    autoLogin: true
    uri: ""/change6""
    nextUri: ""/login?status=reset?""
    view: ""change-password-view""
    errorUri: ""/forgot?status=invalid_sptoken:(""

  # If idSite.enabled is true, the user should be redirected to ID site for
  # login, registration, and password reset.
  idSite:
    enabled: true
    uri: ""/idSiteResultz""
    nextUri: ""/123""
    loginUri: ""/456""
    forgotUri: ""/#/forgot789""
    registerUri: ""/#/register0""

  socialProviders:
    callbackRoot: ""/callbacksYo""

  # The /me route is for front-end applications, it returns a JSON object with
  # the current user object
  me:
    enabled: false
    uri: ""/myself""

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
    enabled: true
    view: indexView

  unauthorized:
    view: ""unauthorizedView""";
            }
        }
    }
}