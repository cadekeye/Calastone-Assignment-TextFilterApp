namespace TextFilterApps.Application.Contracts
{
    public interface ITextFilter
    {
        IEnumerable<string> Apply(IEnumerable<string> words);
    }
}