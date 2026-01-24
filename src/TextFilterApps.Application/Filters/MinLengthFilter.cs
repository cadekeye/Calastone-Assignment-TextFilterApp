using TextFilterApps.Application.Contracts;

namespace TextFilterApps.Application.Filters
{
    // Filter2: Remove words shorter than 3 characters
    public class MinLengthFilter : ITextFilter
    {
        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            return words.Where(w => w.Length >= 3);
        }
    }
}