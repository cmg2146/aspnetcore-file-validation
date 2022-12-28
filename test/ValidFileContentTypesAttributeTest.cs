using Xunit;
using Cmg.AspNetCore.FileValidation;

namespace Cmg.AspnetCore.FileValidation.Test;

public class ValidFileContentTypesAttributeTest : FileValidationAttributeTestBase
{
    [Theory]
    [InlineData(ContentTypeOfFileToTest, "image/png", "image/jpeg")]
    [InlineData(ContentTypeOfFileToTest, "text/plain", "text/csv")]
    public void TestValidContentType(params string[] value)
    {
        var attribute = new ValidFileContentTypesAttribute(value);

        Assert.True(attribute.IsValid(FileToTest), "File content type should be valid");
    }

    [Theory]
    [InlineData("image/png", "image/svg+xml")]
    [InlineData("text/plain", "text/csv")]
    public void TestInvalidContentType(params string[] value)
    {
        var attribute = new ValidFileContentTypesAttribute(value);

        Assert.False(attribute.IsValid(FileToTest), "File content type should be invalid");
    }

    [Theory]
    [InlineData("image/png", "image/svg+xml")]
    [InlineData("text/plain", "text/csv")]
    public void TestValidIfNoFile(params string[] value)
    {
        var attribute = new ValidFileContentTypesAttribute(value);

        Assert.True(attribute.IsValid(null), "Validation should return true if file is null");
    }
}
