namespace TextFilterApps.Application.Contracts;

public interface IFileReaderService
{
    Task<string> ReadFileAsync(string newfilePath);
}