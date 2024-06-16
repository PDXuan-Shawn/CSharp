using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public sealed class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> instance =
        new Lazy<ConfigurationManager>(() => new ConfigurationManager(), LazyThreadSafetyMode.ExecutionAndPublication);

    private Dictionary<string, string> configurations;

    public static ConfigurationManager Instance => instance.Value;

    private ConfigurationManager()
    {
        Console.WriteLine("ConfigurationManager instance created.");
        LoadConfigurations();
    }

    private void LoadConfigurations()
    {
        // Mock: Loadconfig from appsetting.json
        configurations = new Dictionary<string, string>
        {
            {"APIEndpoint", "https://api.example.com"},
            {"IsCheckLicense", "false"}
        };
    }

    public string GetConfiguration(string key)
    {
        configurations.TryGetValue(key, out var value);
        return value;
    }
}

class Program
{
    static void Main()
    {
        // Fake request from multiple thread 
        Parallel.Invoke(
            () => AccessConfigurationManager(),
            () => AccessConfigurationManager(),
            () => AccessConfigurationManager()
        );
    }

    static void AccessConfigurationManager()
    {
        string apiEndpoint = ConfigurationManager.Instance.GetConfiguration("APIEndpoint");
        Console.WriteLine($"API Endpoint: {apiEndpoint}");

        string isCheckLicense = ConfigurationManager.Instance.GetConfiguration("IsCheckLicense");
        Console.WriteLine($"IsCheckLicense: {isCheckLicense}");

    }
}
