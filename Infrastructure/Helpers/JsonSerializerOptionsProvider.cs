using System.Text.Json;

namespace Infrastructure.Helpers;

public sealed class JsonSerializerOptionsProvider
{
    private static readonly JsonSerializerOptions _instance;

    static JsonSerializerOptionsProvider()
    {
        _instance = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public static JsonSerializerOptions GetOptions()
    {
        return _instance;
    }
}
