using TextFilterApps.Application.Contracts;

namespace TextFilterApps.Application.Filters
{
    // Filter2: Remove words shorter than 3 characters
    public class MinLengthFilter : ITextFilter
    {
        private readonly int _minLength = 3;

        public MinLengthFilter()
        {
        }

        public MinLengthFilter(int minLength)
        {
            _minLength = minLength;
        }

        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            return words.Where(w => w.Length >= _minLength);
        }
    }
}