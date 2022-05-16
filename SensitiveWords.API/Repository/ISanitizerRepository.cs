namespace SensitiveWords.API.Repository;

public interface ISanitizerRepository
{
    Task<List<string>> GetListAsync();
}