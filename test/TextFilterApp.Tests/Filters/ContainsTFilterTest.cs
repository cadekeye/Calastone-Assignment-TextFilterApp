using TextFilterApps.Application.Filters;

namespace TextFilterApp.Tests.Filters
{
    public class ContainsTFilterTest
    {
        [Fact]
        public void ContainsTFilter_ThrowsArgumentNullException_When_TextInputIsNull()
        {
            //Arrange
            var textFilter = new ContainsTFilter();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                textFilter.Apply(null!);
            });
        }

        [Fact]
        public void ContainsTFilter_RemovesWordsWithT()
        {
            //Arrange
            var filter = new ContainsTFilter();
            var input = new[] { "cat", "dog", "tree" };

            //Act
            var result = filter.Apply(input).ToArray();

            //Assert
            Assert.DoesNotContain("cat", result);
            Assert.DoesNotContain("tree", result);
            Assert.Contains("dog", result);
        }
    }
}