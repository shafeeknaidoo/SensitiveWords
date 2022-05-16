using Microsoft.EntityFrameworkCore;

namespace SensitiveWords.API.Repository;

public class SanitizerRepository : ISanitizerRepository
{
    private readonly SanitizeRepositoryContext _context;

    public SanitizerRepository(SanitizeRepositoryContext sanitizeRepositoryContext)
    {
        _context = sanitizeRepositoryContext;
    }

    public async Task<List<string>> GetListAsync()
    {
        var data = await _context.Words.Select(w => w.Word).ToListAsync();
        return data;
    }
}