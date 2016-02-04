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
    public class JsonTestCase : TestCaseBase
    {
        public override string TestName => "Json";

        public override string Filename => "stormpath.json";

        public override string FileContents
        {
            get
            {
                return @"
{
  ""client"": {
    ""apiKey"": {
      ""file"": null,
      ""id"": null,
      ""secret"": null
    },
    ""cacheManager"": {
      ""defaultTtl"": 300,
      ""defaultTti"": 300,
      ""caches"": {
        ""account"": {
          ""ttl"": 300,
          ""tti"": 300
        }
      }
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
  ""application"": {
    ""name"": null,
    ""href"": null
  },
  ""web"": {
    ""basePath"": null,
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
        ""validationStrategy"": ""local""
      }
    },
    ""expand"": {
      ""apiKeys"": false,
      ""customData"": true,
      ""directory"": false,
      ""groups"": false
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
      ""text/html"",
      ""application/json""
    ],
    ""register"": {
      ""enabled"": true,
      ""uri"": ""/register"",
      ""nextUri"": ""/"",
      ""autoLogin"": false,
      ""form"": {
        ""fields"": {
          ""givenName"": {
            ""enabled"": true,
            ""label"": ""First Name"",
            ""placeholder"": ""First Name"",
            ""required"": true,
            ""type"": ""text""
          },
          ""middleName"": {
            ""enabled"": false,
            ""label"": ""Middle Name"",
            ""placeholder"": ""Middle Name"",
            ""required"": true,
            ""type"": ""text""
          },
          ""surname"": {
            ""enabled"": true,
            ""label"": ""Last Name"",
            ""placeholder"": ""Last Name"",
            ""required"": true,
            ""type"": ""text""
          },
          ""username"": {
            ""enabled"": false,
            ""label"": ""Username"",
            ""placeholder"": ""Username"",
            ""required"": true,
            ""type"": ""text""
          },
          ""email"": {
            ""enabled"": true,
            ""label"": ""Email"",
            ""placeholder"": ""Email"",
            ""required"": true,
            ""type"": ""email""
          },
          ""password"": {
            ""enabled"": true,
            ""label"": ""Password"",
            ""placeholder"": ""Password"",
            ""required"": true,
            ""type"": ""password""
          },
          ""confirmPassword"": {
            ""enabled"": false,
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
      ""enabled"": null,
      ""uri"": ""/verify"",
      ""nextUri"": ""/login"",
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
            ""label"": ""Username or Email"",
            ""placeholder"": ""Username or Email"",
            ""required"": true,
            ""type"": ""text""
          },
          ""password"": {
            ""enabled"": true,
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
      ""enabled"": null,
      ""autoLogin"": false,
      ""uri"": ""/change"",
      ""nextUri"": ""/login?status=reset"",
      ""view"": ""change-password"",
      ""errorUri"": ""/forgot?status=invalid_sptoken""
    },
    ""idSite"": {
      ""enabled"": false,
      ""uri"": ""/idSiteResult"",
      ""nextUri"": ""/"",
      ""loginUri"": """",
      ""forgotUri"": ""/#/forgot"",
      ""registerUri"": ""/#/register""
    },
    ""socialProviders"": {
      ""callbackRoot"": ""/callbacks""
    },
    ""me"": {
      ""enabled"": true,
      ""uri"": ""/me""
    },
    ""spa"": {
      ""enabled"": false,
      ""view"": ""index""
    },
    ""unauthorized"": {
      ""view"": ""unauthorized""
    }
  }
}";
            }
        }
    }
}
