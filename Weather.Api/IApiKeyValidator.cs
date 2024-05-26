namespace Weather.Api;

public interface IApiKeyValidator
{
    bool Validate(string apiKey);
}
