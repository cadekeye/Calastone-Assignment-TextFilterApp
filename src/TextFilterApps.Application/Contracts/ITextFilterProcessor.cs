namespace TextFilterApps.Application.Contracts;

public interface ITextFilterProcessor
{
    Task<string> ProcessFileAsync(ITextFilter[] filters, string? filePath = null);
}