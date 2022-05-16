using Microsoft.AspNetCore.Mvc;
using SensitiveWords.API.Services;

namespace SensitiveWords.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensitiveWordsController : ControllerBase
{
    private readonly ISanitizerService _sanitizerService;

    public SensitiveWordsController(ISanitizerService sanitizerService)
    {
        _sanitizerService = sanitizerService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> GetWordsAsync([FromBody] string text)
    {
        var sanitizedString = await _sanitizerService.SanitizeAsync(text);
        return sanitizedString;
    }
}