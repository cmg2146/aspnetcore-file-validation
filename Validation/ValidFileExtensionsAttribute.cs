using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Data validation attribute for validating file extensions of an IFormFile or IFormFileCollection
/// </summary>
public class ValidFileExtensionsAttribute : FileValidationAttributeBase
{
    private readonly string _formattedExtensions;

    /// <summary>
    /// File Extensions data validation attribute constructor
    /// </summary>
    /// <param name="extensions">List of valid file extensions. Include the dot.</param>
    public ValidFileExtensionsAttribute(params string[] extensions)
    {
        var loweredExtensions = extensions.Select(e => e.ToLowerInvariant()).ToArray();

        _formattedExtensions = string.Join(", ", loweredExtensions);
        ValidExtensions = loweredExtensions;
    }

    /// <summary>
    /// Valid file extensions. Extension must being with a dot.
    /// </summary>
    public string[] ValidExtensions { get; }

    /// <inheritdoc />
    protected override string GetErrorMessage(IFormFile file) =>
        $"{file.FileName} does not have a valid extension. Valid Extensions: {_formattedExtensions}.";

    /// <inheritdoc />
    protected override string GetErrorMessage(IFormFileCollection files) =>
        $"One of the files does not have a valid extension. Valid Extensions: {_formattedExtensions}.";

    /// <summary>
    /// Checks if a file has a valid extension
    /// </summary>
    protected override bool IsValid(IFormFile file)
    {
        return ValidExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant());
    }
}
