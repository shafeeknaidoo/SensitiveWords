using Moq;
using SensitiveWords.API.Repository;
using SensitiveWords.API.Repository.Models;
using SensitiveWords.API.Services;

namespace SensitiveWordsAPI.Test;

public class SanitizerTest
{
    private readonly ISanitizerService _sanitizerService;
    private readonly Mock<ISanitizerRepository> _sanitizerRepository;
    
    public SanitizerTest()
    {
        var blacklist = new List<string>
        {
            "ACTION",
            "ADD",
            "ALL",
            "COMMIT",
            "CONNECT",
            "SELECT",
            "DECLARE",
            "ALICE",
            "RUBY"
        };
        _sanitizerRepository = new Mock<ISanitizerRepository>();
        _sanitizerRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(blacklist));
        _sanitizerService = new SanitizerService(_sanitizerRepository.Object);
    }
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestSanitizeSingle()
    {
        var newText = await _sanitizerService.SanitizeAsync("I am a man of action");
        Assert.That(newText, Is.EqualTo("I am a man of ******"));
    }
    
    [Test]
    public async Task TestSanitizeMultiple()
    {
        var newText = await _sanitizerService.SanitizeAsync("I am a man of action, I commit and connect. Before I select a database I Declare my intention");
        Assert.That(newText, Is.EqualTo("I am a man of ******, I ****** and *******. Before I ****** a database I ******* my intention"));
    }
    
    [Test]
    public async Task TestSanitizeMultipleUpperCASE()
    {
        var newText = await _sanitizerService.SanitizeAsync("I AM A MAN OF ACTION, I COMMIT AND CONNECT. BEFORE I SELECT A DATABASE I DECLARE MY INTENTION");
        Assert.That(newText, Is.EqualTo("I AM A MAN OF ******, I ****** AND *******. BEFORE I ****** A DATABASE I ******* MY INTENTION"));
    }
    
    [Test]
    public async Task TestSanitizeMultipleMixedCASE()
    {
        var newText = await _sanitizerService.SanitizeAsync("i am a man OF ACTION, I COMMIT AND CONNECT. before i select a database i declare MY INTENTION");
        Assert.That(newText, Is.EqualTo("i am a man OF ******, I ****** AND *******. before i ****** a database i ******* MY INTENTION"));
    }
    
    [Test]
    public async Task TestSanitizeNoChange()
    {
        var newText = await _sanitizerService.SanitizeAsync("The big brown fox jumped over the fence");
        Assert.That(newText, Is.EqualTo("The big brown fox jumped over the fence"));
    }
    
    [Test]
    public async Task TestSanitizeEmptyString()
    {
        var newText = await _sanitizerService.SanitizeAsync("");
        Assert.That(newText, Is.EqualTo(""));
    }
    
    [Test]
    public async Task TestSanitizeDuplicates()
    {
        var newText = await _sanitizerService.SanitizeAsync("I am a man of action, action, action");
        Assert.That(newText, Is.EqualTo("I am a man of ******, ******, ******"));
    }
    
    [Test]
    public async Task TestSanitizePartials()
    {
        var newText = await _sanitizerService.SanitizeAsync("I am a man of actions");
        Assert.That(newText, Is.EqualTo("I am a man of actions"));
    }
    
    
}