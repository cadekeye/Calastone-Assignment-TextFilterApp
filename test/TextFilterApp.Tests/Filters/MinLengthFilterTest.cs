using TextFilterApps.Application.Filters;

namespace TextFilterApp.Tests.Filters
{
    public class MinLengthFilterTest
    {
        [Fact]
        public void MinLengthFilter_ThrowsArgumentNullException_When_TextInputIsNull()
        {
            //Arrange
            var textFilter = new MinLengthFilter();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                textFilter.Apply(null!);
            });
        }

        [Fact]
        public void MinLengthFilter_RemovesShortWords()
        {
            var filter = new MinLengthFilter();
            var input = new[] { "hi", "cat", "a", "dog" };
            var result = filter.Apply(input).ToArray();

            Assert.DoesNotContain("hi", result);
            Assert.DoesNotContain("a", result);
            Assert.Contains("cat", result);
            Assert.Contains("dog", result);
        }
    }
}