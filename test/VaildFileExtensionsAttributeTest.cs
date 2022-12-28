using Xunit;
using Cmg.AspNetCore.FileValidation;

namespace Cmg.AspnetCore.FileValidation.Test;

public class ValidFileExtensionsAttributeTest : FileValidationAttributeTestBase
{
    [Theory]
    [InlineData(ExtensionOfFileToTest, ".gif", ".txt")]
    [InlineData(ExtensionOfFileToTest, ".png", ".doc")]
    public void TestValidExtension(params string[] value)
    {
        var attribute = new ValidFileExtensionsAttribute(value);

        Assert.True(attribute.IsValid(FileToTest), "File extension should be valid");
    }

    [Theory]
    [InlineData(".gif", ".txt")]
    [InlineData(".png", ".doc")]
    public void TestInvalidExtension(params string[] value)
    {
        var attribute = new ValidFileExtensionsAttribute(value);

        Assert.False(attribute.IsValid(FileToTest), "File extension should be invalid");
    }

    [Theory]
    [InlineData(".gif", ".txt")]
    [InlineData(".png", ".doc")]
    public void TestValidIfNoFile(params string[] value)
    {
        var attribute = new ValidFileExtensionsAttribute(value);

        Assert.True(attribute.IsValid(null), "Validation should return true if file is null");
    }
}
