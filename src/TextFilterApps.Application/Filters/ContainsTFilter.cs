using TextFilterApps.Application.Contracts;

namespace TextFilterApps.Application.Filters
{
    // Filter3: Remove words containing 't'
    public class ContainsTFilter : ITextFilter
    {
        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            return words.Where(w => !w.ToLower().Contains('t'));
        }
    }
}