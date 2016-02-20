# stormpath-dotnet-config [![Build status](https://ci.appveyor.com/api/projects/status/740yactt0g8xpgfi?svg=true)](https://ci.appveyor.com/project/nbarbettini/stormpath-dotnet-config)
*Stormpath configuration loader for .NET*

This library is responsible for loading the Stormpath configuration. It is an internal module used by the [Stormpath .NET SDK](https://github.com/stormpath/stormpath-sdk-dotnet) and [ASP.NET integration](https://github.com/stormpath/stormpath-aspnet), and is not meant for general consumption.

## Installing

```
install-package Stormpath.Configuration
```

## Usage

To load the default configuration:
```
var configuration = ConfigurationLoader.Load();
```

To load the default configuration and override specific items:
```
var configuration = ConfigurationLoader.Load(new StormpathConfiguration()
{
  Client = new ClientConfiguration()
  {
    ApiKey = new ClientApiKeyConfiguration()
    {
      File = "path\\to\\apiKey.properties",
    }
  }
});
```

## Support

This library is maintained by [Stormpath](http://stormpath.com). If you have trouble using it, please reach out to [support@stormpath.com](mailto:support@stormpath.com).
