using Microsoft.Extensions.Logging;
using TextFilterApps.Application.Contracts;
using TextFilterApps.Application.Filters;
using TextFilterApps.Application.Services;

namespace TextFilterApps.Presentation.FilterApp;

public class FilterApp : IFilterApp
{
    private readonly ITextFilterProcessor _textFilterProcessor;

    public FilterApp(ITextFilterProcessor textFilterProcessor)
    {
        _textFilterProcessor = textFilterProcessor;
    }

    public async Task HandleTextFilter(string filePath)
    {
        try
        {
            ITextFilter[] filters = new ITextFilter[]
            {
                new MinLengthFilter(),
                new ContainsTFilter(),
                new VowelMiddleFilter()
            };

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File '{filePath}' not found.");
                return;
            }

            var filteredText = await _textFilterProcessor.ProcessFileAsync(filters, filePath);

            Console.WriteLine("Filtered Text:");
            Console.WriteLine(filteredText);
        }
        catch (IOException e)
        {
            Console.WriteLine("Error reading file: " + e.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}