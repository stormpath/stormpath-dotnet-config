# Changelog

## Version 7.0.0

This version supports the migration of ASP.NET applications from Stormpath to Okta. For more information, see the master changelog (todo).

## Breaking changes

* The `client` section was removed
* The `application` section now only includes one property: `id` (the Okta Application ID)
* The configuration parser no longer throws errors if the Stormpath API Key and Secret are missing
* The configuration parser no longer looks for an `apiKey.properties` file