using System.Text.RegularExpressions;
using TextFilterApps.Application.Filters;
using TextFilterApps.Application.Tests.Constants;
using static TextFilterApps.Application.Tests.Constants.Constants;

namespace TextFilterApp.Tests.Filters
{
    public class VowelMiddleFilterTest
    {
        [Fact]
        public void VowelMiddleFilter_ThrowsArgumentNullException_When_TextInputIsNull()
        {
            //Arrange
            var textFilter = new TextFilterApps.Application.Filters.VowelMiddleFilter();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                textFilter.Apply(null!).ToArray();
            });
        }

        [Fact]
        public void VowelMiddleFilter_RemovesWordsWithMiddleVowel()
        {
            var filter = new TextFilterApps.Application.Filters.VowelMiddleFilter();
            var input = new[] { "clean", "what", "the", "rather" };
            var result = filter.Apply(input).ToArray();

            Assert.DoesNotContain("clean", result);
            Assert.DoesNotContain("what", result);
            Assert.Contains("the", result);
            Assert.Contains("rather", result);
        }

        [Theory]
        [MemberData(nameof(ValidMiddleCharExcludeWordRules))]
        public void VowelMiddleFilter_ReturnsExpectedResult(ExcludeFilterTestConditions testConditions)
        {
            //Arrange
            var service = new TextFilterApps.Application.Filters.VowelMiddleFilter();

            //Act
            var words = Regex.Matches(testConditions.Content, @"\b[\w']+\b")
                    .Select(m => m.Value);

            var result = service.Apply(words);

            //Assert
            Assert.Equal(testConditions.Expected, string.Join(" ", result));
        }

        public static IEnumerable<object[]> ValidMiddleCharExcludeWordRules()
        {
            yield return new[] { Constants.VowelMiddleFilter.Expected1 };
            yield return new[] { Constants.VowelMiddleFilter.Expected2 };
            yield return new[] { Constants.VowelMiddleFilter.Expected3 };
            yield return new[] { Constants.VowelMiddleFilter.Expected4 };
            yield return new[] { Constants.VowelMiddleFilter.Expected5 };
            yield return new[] { Constants.VowelMiddleFilter.Expected6 };
            yield return new[] { Constants.VowelMiddleFilter.Expected7 };
        }
    }
}