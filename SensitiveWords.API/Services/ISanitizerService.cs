namespace SensitiveWords.API.Services;

public interface ISanitizerService
{
    Task<string> SanitizeAsync(string text);
}