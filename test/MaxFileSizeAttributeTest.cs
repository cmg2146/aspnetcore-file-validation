using Xunit;
using Cmg.AspNetCore.FileValidation;

namespace Cmg.AspnetCore.FileValidation.Test;

public class MaxFileSizeAttributeTest : FileValidationAttributeTestBase
{
    [Theory]
    [InlineData(SizeOfFileToTest)]
    [InlineData(SizeOfFileToTest + 1)]
    [InlineData(SizeOfFileToTest + 100)]
    public void TestValidBelowMaxSize(long value)
    {
        var attribute = new MaxFileSizeAttribute(value);

        Assert.True(attribute.IsValid(FileToTest), "File size should be valid");
    }

    [Theory]
    [InlineData(SizeOfFileToTest - 1)]
    [InlineData(SizeOfFileToTest - 100)]
    public void TestInvalidAboveMaxSize(long value)
    {
        var attribute = new MaxFileSizeAttribute(value);

        Assert.False(attribute.IsValid(FileToTest), "File size should be invalid");
    }

    [Theory]
    [InlineData(SizeOfFileToTest - 1)]
    [InlineData(SizeOfFileToTest - 100)]
    public void TestValidIfNoFile(long value)
    {
        var attribute = new MaxFileSizeAttribute(value);

        Assert.True(attribute.IsValid(null), "Validation should return true if file is null");
    }
}
