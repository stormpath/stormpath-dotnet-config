# Changelog

## Version 7.0.0

This version supports the migration of ASP.NET applications from Stormpath to Okta. For more information, see the master changelog (todo).

## Breaking changes

* The `stormpath.okta` configuration section was added
* The `stormpath.application` and `stormpath.client` sections were removed
* The configuration parser no longer throws errors if the Stormpath API Key and Secret are missing
* The configuration parser no longer looks for an `apiKey.properties` file