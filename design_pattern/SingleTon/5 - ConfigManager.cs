using System.Collections.Generic;
using System.IO;


// Seadled keyword
public sealed class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> instance =
        new Lazy<ConfigurationManager>(() => new ConfigurationManager());

    private Dictionary<string, string> configurations;

    public static ConfigurationManager Instance => instance.Value;

    private ConfigurationManager()
    {
        LoadConfigurations();
    }

    private void LoadConfigurations()
    {
        // Mock: In a real scenario, you might be reading this from a file or a database.
        configurations = new Dictionary<string, string>
        {
            {"APIEndpoint", "https://api.example.com"},
            {"MaxUsers", "1000"},
            {"RetryCount", "3"}
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
        string apiEndpoint = ConfigurationManager.Instance.GetConfiguration("APIEndpoint");
        Console.WriteLine($"API Endpoint: {apiEndpoint}");
    }
}
