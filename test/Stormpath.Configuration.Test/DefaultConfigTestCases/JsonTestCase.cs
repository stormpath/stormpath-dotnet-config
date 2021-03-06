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

namespace Stormpath.Configuration.Test.DefaultConfigTestCases
{
    public class JsonTestCase : ConfigTestCaseBase
    {
        public override string TestName => "Json";

        public override string Filename => "stormpath.json";

        public override string FileContents
        {
            get
            {
                // Note: Providing values for client.apiKey.id and client.apiKey.secret
                // because validation will fail without them.

                return @"
{
  ""client"": {
    ""apiKey"": {
      ""file"": null,
      ""id"": ""default-foobar"",
      ""secret"": ""default-secret123!""
    },
    ""cacheManager"": {
      ""enabled"": true,
      ""defaultTtl"": 3600,
      ""defaultTti"": 3600,
      ""caches"": { }
    },
    ""baseUrl"": ""https://api.stormpath.com/v1"",
    ""connectionTimeout"": 30,
    ""authenticationScheme"": ""SAUTHC1"",
    ""proxy"": {
      ""port"": null,
      ""host"": null,
      ""username"": null,
      ""password"": null
    }
  },
  ""apiToken"": ""okta_apiToken"",
  ""org"": ""okta_org"",
  ""application"": {
    ""id"": ""okta_application_id""
  },
  ""authorizationServerId"": null,
  ""web"": {
    ""serverUri"": null,
    ""basePath"": ""/"",
    ""oauth2"": {
      ""enabled"": true,
      ""uri"": ""/oauth/token"",
      ""client_credentials"": {
        ""enabled"": true,
        ""accessToken"": {
          ""ttl"": 3600
        }
      },
      ""password"": {
        ""enabled"": true,
        ""defaultScope"": ""openid offline_access"",
        ""validationStrategy"": ""local""
      }
    },
    ""accessTokenCookie"": {
      ""name"": ""access_token"",
      ""httpOnly"": true,
      ""secure"": null,
      ""path"": null,
      ""domain"": null
    },
    ""refreshTokenCookie"": {
      ""name"": ""refresh_token"",
      ""httpOnly"": true,
      ""secure"": null,
      ""path"": null,
      ""domain"": null
    },
    ""produces"": [
      ""application/json"",
      ""text/html""
    ],
    ""register"": {
      ""enabled"": true,
      ""uri"": ""/register"",
      ""nextUri"": ""/"",
      ""autoLogin"": false,
      ""emailVerificationRequired"": false,
      ""form"": {
        ""fields"": {
          ""givenName"": {
            ""enabled"": true,
            ""visible"": true,
            ""label"": ""First Name"",
            ""placeholder"": ""First Name"",
            ""required"": true,
            ""type"": ""text""
          },
          ""middleName"": {
            ""enabled"": false,
            ""visible"": true,
            ""label"": ""Middle Name"",
            ""placeholder"": ""Middle Name"",
            ""required"": true,
            ""type"": ""text""
          },
          ""surname"": {
            ""enabled"": true,
            ""visible"": true,
            ""label"": ""Last Name"",
            ""placeholder"": ""Last Name"",
            ""required"": true,
            ""type"": ""text""
          },
          ""username"": {
            ""enabled"": false,
            ""visible"": true,
            ""label"": ""Username"",
            ""placeholder"": ""Username"",
            ""required"": true,
            ""type"": ""text""
          },
          ""email"": {
            ""enabled"": true,
            ""visible"": true,
            ""label"": ""Email"",
            ""placeholder"": ""Email"",
            ""required"": true,
            ""type"": ""email""
          },
          ""password"": {
            ""enabled"": true,
            ""visible"": true,
            ""label"": ""Password"",
            ""placeholder"": ""Password"",
            ""required"": true,
            ""type"": ""password""
          },
          ""confirmPassword"": {
            ""enabled"": false,
            ""visible"": true,
            ""label"": ""Confirm Password"",
            ""placeholder"": ""Confirm Password"",
            ""required"": true,
            ""type"": ""password""
          }
        },
        ""fieldOrder"": [
          ""username"",
          ""givenName"",
          ""middleName"",
          ""surname"",
          ""email"",
          ""password"",
          ""confirmPassword""
        ]
      },
      ""view"": ""register""
    },
    ""verifyEmail"": {
      ""enabled"": false,
      ""uri"": ""/verify"",
      ""nextUri"": ""/login?status=verified"",
      ""view"": ""verify""
    },
    ""login"": {
      ""enabled"": true,
      ""uri"": ""/login"",
      ""nextUri"": ""/"",
      ""view"": ""login"",
      ""form"": {
        ""fields"": {
          ""login"": {
            ""enabled"": true,
            ""visible"": true,
            ""label"": ""Username or Email"",
            ""placeholder"": ""Username or Email"",
            ""required"": true,
            ""type"": ""text""
          },
          ""password"": {
            ""enabled"": true,
            ""visible"": true,
            ""label"": ""Password"",
            ""placeholder"": ""Password"",
            ""required"": true,
            ""type"": ""password""
          }
        },
        ""fieldOrder"": [
          ""login"",
          ""password""
        ]
      }
    },
    ""logout"": {
      ""enabled"": true,
      ""uri"": ""/logout"",
      ""nextUri"": ""/""
    },
    ""forgotPassword"": {
      ""enabled"": null,
      ""uri"": ""/forgot"",
      ""view"": ""forgot-password"",
      ""nextUri"": ""/login?status=forgot""
    },
    ""changePassword"": {
      ""enabled"": false,
      ""uri"": ""/change"",
      ""nextUri"": ""/login?status=reset"",
      ""view"": ""change-password"",
      ""errorUri"": ""/forgot?status=invalid_sptoken""
    },
    ""idSite"": {
      ""enabled"": false,
      ""loginUri"": """",
      ""forgotUri"": ""/#/forgot"",
      ""registerUri"": ""/#/register""
    },
    ""callback"": {
      ""enabled"": true,
      ""uri"": ""/stormpathCallback""
    },
    ""social"": {
        ""facebook"": {
            ""displayName"": ""Facebook"",
            ""scope"": ""openid""
        },
        ""google"": {
            ""displayName"": ""Google"",
            ""scope"": ""openid""
        },
        ""linkedin"": {
            ""displayName"": ""LinkedIn"",
            ""scope"": ""openid""
        }
    },
    ""me"": {
      ""enabled"": true,
      ""uri"": ""/me"",
      ""expand"": {
        ""apiKeys"": false,
        ""applications"": false,
        ""customData"": false,
        ""directory"": false,
        ""groupMemberships"": false,
        ""groups"": false,
        ""providerData"": false,
        ""tenant"": false
      }
    }
  }
}";
            }
        }
    }
}
