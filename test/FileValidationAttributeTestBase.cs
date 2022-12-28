using Microsoft.AspNetCore.Http;
using Xunit;

namespace Cmg.AspnetCore.FileValidation.Test;

public class FileValidationAttributeTestBase
{
    public IFormFile FileToTest { get; init; }
    public const string NameOfFileToTest = "dc.jpg";
    public const long SizeOfFileToTest = 3047354;
    public const string ExtensionOfFileToTest = ".JPG";
    public const string ContentTypeOfFileToTest = "image/jpeg";

    public FileValidationAttributeTestBase()
    {
        using var stream = File.OpenRead(NameOfFileToTest);

        FileToTest = new FormFile(
            stream,
            0,
            SizeOfFileToTest,
            $"upload{ExtensionOfFileToTest}",
            NameOfFileToTest
        )
        {
            Headers = new HeaderDictionary(),
            ContentType = ContentTypeOfFileToTest
        };
    }
}
