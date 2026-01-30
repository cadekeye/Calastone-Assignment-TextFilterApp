using TextFilterApps.Application.Contracts;

namespace TextFilterApps.Application.Filters
{
    // Filter3: Remove words containing 't'
    public class ContainsTFilter : ITextFilter
    {
        private readonly string _filterString;

        public ContainsTFilter()
        {
            _filterString = "t";
        }

        public ContainsTFilter(string filterCharacter)
        {
            _filterString = filterCharacter;
        }

        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            return words.Where(w => !w.ToLower().Contains(_filterString));
        }
    }
}