using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace FeatureFlagDemo;

public static class AwsAppConfigExtensions
{
    public static IConfigurationBuilder AddAwsAppConfig(this IConfigurationBuilder builder)
    {
        // Create and configure your custom configuration source here
        var source = new AwsAppConfigSource();
        // Add the custom configuration source to the builder
        builder.Add(source);
        return builder;
    }
}

public class AwsAppConfigSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        // Create and configure your custom configuration provider here
        var provider = new AwsAppConfigProvider();
        return provider;
    }
}

public class AwsAppConfigProvider : ConfigurationProvider 
{
    private readonly Timer _timer;
    
    public AwsAppConfigProvider() 
    {
        _timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
    }
    
    private void OnTimerElapsed(object state) 
    {
        Load();
    }

    public override void Load() 
    {
        // Load the configuration data from your custom location here
        var data = new Dictionary<string, string> 
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };

        // Set the Data property to the loaded configuration data
        Data = data;
    }
}