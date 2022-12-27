using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Data validation attribute for validating file size of an IFormFile or IFormFileCollection
/// </summary>
public class MaxFileSizeAttribute : FileValidationAttributeBase
{
    /// <summary>
    /// Max file size data validation attribute constructor
    /// </summary>
    /// <param name="maxFileSizeBytes">Max valid file size in bytes.</param>
    public MaxFileSizeAttribute(long maxFileSizeBytes)
    {
        MaxFileSizeBytes = maxFileSizeBytes;
    }

    /// <summary>
    /// Max file size in bytes.
    /// </summary>
    public long MaxFileSizeBytes { get; }

    /// <inheritdoc />
    protected override string GetErrorMessage(IFormFile file) =>
        $"{file.FileName} exceeds maximum allowed file size of {MaxFileSizeBytes} bytes.";

    /// <inheritdoc />
    protected override string GetErrorMessage(IFormFileCollection files) =>
        $"One of the files exceeds the maximum allowed file size of {MaxFileSizeBytes} bytes.";

    /// <summary>
    /// Checks if a file is within the size limit
    /// </summary>
    protected override bool IsValid(IFormFile file)
    {
        return file.Length <= MaxFileSizeBytes;
    }
}
