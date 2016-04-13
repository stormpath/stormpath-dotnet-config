# Stormpath .NET Configuration

This library is responsible for loading and representing the Stormpath SDK configuration. It is an internal module used by the [Stormpath .NET SDK](https://github.com/stormpath/stormpath-sdk-dotnet) and [OWIN middleware](https://github.com/stormpath/stormpath-dotnet-owin-middleware) components.

## Installing

```
install-package Stormpath.Configuration
```

## Usage

To load the default configuration:
```csharp
var configuration = ConfigurationLoader.Initialize().Load();
```

To load the default configuration and override specific items:
```csharp
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
