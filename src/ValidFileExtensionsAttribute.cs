using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Data validation attribute for validating file extensions of an IFormFile or IFormFileCollection
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidFileExtensionsAttribute : FileValidationAttribute
{
    /// <summary>
    /// File Extensions data validation attribute constructor
    /// </summary>
    /// <param name="extensions">List of valid file extensions. Include the dot.</param>
    public ValidFileExtensionsAttribute(params string[] extensions)
        : base(() => DefaultErrorMessageString)
    {
        var lowerCaseExtensions = extensions.Select(e => e.ToLowerInvariant()).ToArray();

        _formattedExtensions = string.Join(", ", lowerCaseExtensions);
        ValidExtensions = lowerCaseExtensions;
    }

    /// <summary>
    /// Valid file extensions. Extension must being with a dot.
    /// </summary>
    public string[] ValidExtensions { get; }

    private static readonly string DefaultErrorMessageString =
        "Invalid file extension in '{0}' field. Valid extensions: '{1}'.";

    private readonly string _formattedExtensions;

    /// <summary>
    /// Checks if a file has a valid extension
    /// </summary>
    protected override bool IsValid(IFormFile file)
    {
        return ValidExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant());
    }

    /// <summary>
    /// Override of <see cref="ValidationAttribute.FormatErrorMessage"/>
    /// </summary>
    /// <param name="name">The name to include in the formatted string</param>
    /// <returns>A localized string to describe the allowed file extensions</returns>
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _formattedExtensions);
    }
}
