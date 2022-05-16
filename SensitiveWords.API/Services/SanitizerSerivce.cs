using System.Text.RegularExpressions;
using SensitiveWords.API.Repository;

namespace SensitiveWords.API.Services;

public class SanitizerService : ISanitizerService
{
    private readonly ISanitizerRepository _sanitizerRepository;
    private List<string> _blacklist;

    public SanitizerService(ISanitizerRepository sanitizerRepository)
    {
        _sanitizerRepository = sanitizerRepository;
    }

    public async Task<string> SanitizeAsync(string text)
    {
        _blacklist = await _sanitizerRepository.GetListAsync();
        _blacklist = _blacklist.ConvertAll(s => s.ToLower());

        foreach (var word in _blacklist)
        {
            var pattern = $@"\b{word}\b";
            text = Regex.Replace(text, pattern, TransformBlacklistWord(word), RegexOptions.IgnoreCase);
        }

        return text;
    }

    private string TransformBlacklistWord(string word)
    {
        var bleepedWord = "";

        for (var i = 0; i < word.Length; i++) bleepedWord = bleepedWord + "*";

        return bleepedWord;
    }
}