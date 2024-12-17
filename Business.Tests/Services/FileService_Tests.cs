using Business.Interfaces;
using Moq;

namespace Business.Tests.Services;

public class FileService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IFileService _fileService;

    public FileService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _fileService = _fileServiceMock.Object;
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnTrue()
    {
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);

        var result = _fileService.SaveContentToFile("");

        Assert.True(result);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnContentAsString()
    {
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns("test");

        var result = _fileService.GetContentFromFile();

        Assert.Equal("test", result);
    }

}
