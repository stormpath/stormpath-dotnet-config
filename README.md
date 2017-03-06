#Stormpath is Joining Okta
We are incredibly excited to announce that [Stormpath is joining forces with Okta](https://stormpath.com/blog/stormpaths-new-path?utm_source=github&utm_medium=readme&utm-campaign=okta-announcement). Please visit [the Migration FAQs](https://stormpath.com/oktaplusstormpath?utm_source=github&utm_medium=readme&utm-campaign=okta-announcement) for a detailed look at what this means for Stormpath users.

We're available to answer all questions at [support@stormpath.com](mailto:support@stormpath.com).


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
