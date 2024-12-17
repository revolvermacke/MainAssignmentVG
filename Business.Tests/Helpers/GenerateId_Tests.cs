using Business.Helpers;

namespace Business.Tests.Helpers;

public class GenerateId_Tests
{
    [Fact]
    public void GenerateUniqueId_ShouldReturnGuidAsString()
    {
        var result = UniqueIDGenerator.GenerateUniqueId();

        Assert.True(Guid.TryParse(result, out _));
    }
}
