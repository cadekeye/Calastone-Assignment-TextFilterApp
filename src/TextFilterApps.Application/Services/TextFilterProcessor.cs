using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using TextFilterApps.Application.Contracts;

namespace TextFilterApps.Application.Services;

public class TextFilterProcessor : ITextFilterProcessor
{
    private readonly ILogger<TextFilterProcessor> _logger;
    private readonly IFileReaderService _fileReaderService;

    public TextFilterProcessor(ILogger<TextFilterProcessor> logger, IFileReaderService fileReaderService)
    {
        _logger = logger;
        _fileReaderService = fileReaderService;
    }

    public async Task<string> ProcessFileAsync(ITextFilter[] filters, string? filePath = null)
    {
        var textContexts = await ReadFileAsync(filePath);
        return ApplyFilters(textContexts, filters);
    }

    private async Task<string> ReadFileAsync(string? filePath = null)
    {
        return await _fileReaderService.ReadFileAsync(filePath!);
    }

    private string ApplyFilters(string content, ITextFilter[] filters)
    {
        var filteredContent = Regex.Matches(content, @"\b[\w']+\b")
                    .Select(m => m.Value);
        try
        {
            foreach (var filter in filters)
            {
                filteredContent = filter.Apply(filteredContent);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred applying filters");
            throw;
        }

        return string.Join(" ", filteredContent);
    }
}