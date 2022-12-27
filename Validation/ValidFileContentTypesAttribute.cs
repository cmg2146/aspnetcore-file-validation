using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Data validation attribute for validating file content types of an IFormFile or IFormFileCollection
/// </summary>
public class ValidFileContentTypesAttribute : FileValidationAttributeBase
{
    private readonly string _formattedTypes;

    /// <summary>
    /// File content types data validation attribute constructor
    /// </summary>
    /// <param name="contentTypes">List of valid file content types.</param>
    public ValidFileContentTypesAttribute(params string[] contentTypes)
    {
        var loweredTypes = contentTypes.Select(e => e.ToLowerInvariant()).ToArray();

        _formattedTypes = string.Join(", ", loweredTypes);
        ValidContentTypes = loweredTypes;
    }

    /// <summary>
    /// Valid file content types.
    /// </summary>
    public string[] ValidContentTypes { get; }

    /// <inheritdoc />
    protected override string GetErrorMessage(IFormFile file) =>
        $"{file.FileName} does not have a valid type. Valid Content Types: {_formattedTypes}.";

    /// <inheritdoc />
    protected override string GetErrorMessage(IFormFileCollection files) =>
        $"One of the files does not have a valid type. Valid Content Types: {_formattedTypes}.";

    /// <summary>
    /// Checks if a file has a valid content type
    /// </summary>
    protected override bool IsValid(IFormFile file)
    {
        return ValidContentTypes.Contains(file.ContentType.ToLowerInvariant());
    }
}
