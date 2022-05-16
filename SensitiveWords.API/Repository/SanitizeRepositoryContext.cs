using Microsoft.EntityFrameworkCore;
using SensitiveWords.API.Repository.Models;

namespace SensitiveWords.API.Repository;

public class SanitizeRepositoryContext : DbContext
{
    private readonly string _dbPath;


    public SanitizeRepositoryContext(DbContextOptions<SanitizeRepositoryContext> options)
        : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        _dbPath = "Repository/sanitize.db";
        //_dbPath = System.IO.Path.Join(path, "sanitize.db");
    }

    public DbSet<SpecialWord> Words { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={_dbPath}");
    }
}