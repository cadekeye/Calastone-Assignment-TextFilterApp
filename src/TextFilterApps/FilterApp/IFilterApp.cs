namespace TextFilterApps.Presentation.FilterApp;

public interface IFilterApp
{
    Task HandleTextFilter(string filePath);
}