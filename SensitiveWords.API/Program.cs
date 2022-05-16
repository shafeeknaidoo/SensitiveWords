using Microsoft.EntityFrameworkCore;
using SensitiveWords.API.Repository;
using SensitiveWords.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddDbContext<SanitizeRepositoryContext>(opt =>
    opt.UseSqlite());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISanitizerRepository, SanitizerRepository>();
builder.Services.AddScoped<ISanitizerService, SanitizerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();