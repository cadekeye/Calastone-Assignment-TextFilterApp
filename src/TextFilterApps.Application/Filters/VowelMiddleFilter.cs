using System.Text;
using System.Text.RegularExpressions;
using TextFilterApps.Application.Contracts;

namespace TextFilterApps.Application.Filters
{
    // Filter1: Remove words with a vowel in the middle
    public partial class VowelMiddleFilter : ITextFilter
    {
        private static readonly HashSet<char> Vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            ArgumentNullException.ThrowIfNull(words);

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                var lowerWord = word.ToLower();
                int len = lowerWord.Length;

                // Determine middle index(es)
                if (len % 2 == 1) // Odd length → single middle char
                {
                    int mid = len / 2;
                    if (!Vowels.Contains(lowerWord[mid]))
                        yield return word;
                }
                else // Even length → two middle chars
                {
                    int mid1 = (len / 2) - 1;
                    int mid2 = len / 2;
                    if (!Vowels.Contains(lowerWord[mid1]) && !Vowels.Contains(lowerWord[mid2]))
                        yield return word;
                }
            }
        }
    }
}