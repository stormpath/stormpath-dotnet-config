# Changelog

## Version 7.1.0

* Added the `web.refreshTokenCookie.maxAge` property, which controls the expiration time (if any) of refresh token cookies.
* Added the `authorizationServerId` property, which allows the developer to specify the Okta Authorization Server to use (instead of relying on auto-discovery).
* Added the `web.oauth2.password.defaultScope` property, which sets the default scopes (if any) that are sent during a Password Grant request.

## Version 7.0.0

No breaking changes from beta4.

## Version 7.0.0-beta4

* The `web.social.[provider]` items no longer have a `uri` property.
* The YamlDotNet dependency has been updated so it no longer requires a workaround to install on .NET Core.

## Version 7.0.0-beta3

This version supports the migration of ASP.NET applications from Stormpath to Okta. For more information, see the [master changelog](https://github.com/stormpath/stormpath-dotnet-owin-middleware/blob/master/changelog.md).

#### Breaking changes

* The `client` section was removed.
* The `application` section now only includes one property: `id` (the Okta Application ID)
* The `web.idSite` section was removed.
* The `web.changePassword.enabled` and `web.verifyEmail.enabled` settings are now **false** by default.
* The `web.register.emailVerificationRequired` setting was added.
* All configuration properties start with `okta.*` instead of `stormpath.*`. For most applications, this won't cause any issues.
* JSON/YAML configuration can now be loaded from `okta.json` or `okta.yaml` **or** `stormpath.json`/`stormpath.yaml`. For backwards compatibility, either filename will work. (Note: the home directory search location is still `~/.stormpath`, for maximum backwards compatibility.)
* Environment variable configuration can now be specified using `OKTA_` environment variables in addition to `STORMPATH_` environment variables. For backwards compatibility, either string format will work.
* Configuration provided via an `appsettings.json` file must now be placed in an `Okta` section.
* The configuration parser no longer throws errors if the Stormpath API Key and Secret are missing.
* The configuration parser no longer looks for an `apiKey.properties` file.
