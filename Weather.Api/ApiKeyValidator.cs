namespace Weather.Api;

public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IConfiguration _configuration;

    public ApiKeyValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool Validate(string apiKey)
    {
        // Retrieve the expected API key from the app settings
        var expectedApiKeys = _configuration.GetSection("ApiKeys").Get<string[]>();
        //Console.WriteLine(string.Join(',', expectedApiKeys));
        // Compare the provided API key with the expected API key
        return expectedApiKeys.Contains(apiKey);
    }
}
