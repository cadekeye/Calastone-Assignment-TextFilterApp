using Microsoft.Extensions.Logging;
using Moq;
using TextFilterApps.Infrastructure.Services;

namespace TextFilterApps.Infrastructure.Tests;

public class FileReaderServiceTests
{
    private readonly Mock<ILogger<FileReaderService>> _logger;

    public FileReaderServiceTests()
    {
        _logger = new Mock<ILogger<FileReaderService>>();
    }

    private FileReaderService CreateFileReader()
    {
        return new FileReaderService(_logger.Object);
    }

    [Fact]
    public async Task FileReaderService_WhenFileIsAccessible_ReturnContentsOfFile()
    {
        //Arrange
        var service = CreateFileReader();

        //Act
        var result = await service.ReadFileAsync(Constants.Constants.FileReaderService.FilePath1);

        //Assert
        Assert.Equal(Constants.Constants.FileReaderService.FilePath1_CorrectFileContents, result);
    }

    [Fact]
    public async Task FileReaderService_PathIsIncorrect_ThrowFileNotFoundException()
    {
        //Arrange
        var service = CreateFileReader();

        //Act
        var ex = await Assert.ThrowsAsync<FileNotFoundException>(() => service.ReadFileAsync(Constants.Constants.FileReaderService.InvalidFilePath));

        //Assert
        Assert.IsType<FileNotFoundException>(ex);
        Assert.Equal($"Unable to find File path {Constants.Constants.FileReaderService.InvalidFilePath}", ex.Message);
    }

    [Fact]
    public async Task FileReaderService_FileIsEmpty_ThrowExceptionSightingContentsEmpty()
    {
        //Arrange
        var service = CreateFileReader();

        //Act
        var ex = await Assert.ThrowsAsync<Exception>(() => service.ReadFileAsync(Constants.Constants.FileReaderService.EmptyFile));

        //Assert
        Assert.IsType<Exception>(ex);
        Assert.Equal($"The file is empty. File Path: {Constants.Constants.FileReaderService.EmptyFile}.", ex.Message);
    }
}