using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Data validation attribute for validating file content types of an IFormFile or IFormFileCollection
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidFileContentTypesAttribute : FileValidationAttribute
{
    /// <summary>
    /// File content types data validation attribute constructor
    /// </summary>
    /// <param name="contentTypes">List of valid file content types.</param>
    public ValidFileContentTypesAttribute(params string[] contentTypes)
        : base(() => DefaultErrorMessageString)
    {
        var lowerCaseContentTypes = contentTypes.Select(e => e.ToLowerInvariant()).ToArray();

        _formattedTypes = string.Join(", ", lowerCaseContentTypes);
        ValidContentTypes = lowerCaseContentTypes;
    }

    /// <summary>
    /// Valid file content types.
    /// </summary>
    public string[] ValidContentTypes { get; }

    private static readonly string DefaultErrorMessageString =
        "Invalid file content type in '{0}' field. Valid content types: '{1}'.";

    private readonly string _formattedTypes;

    /// <summary>
    /// Checks if a file has a valid content type
    /// </summary>
    protected override bool IsValid(IFormFile file)
    {
        return ValidContentTypes.Contains(file.ContentType.ToLowerInvariant());
    }

    /// <summary>
    /// Override of <see cref="ValidationAttribute.FormatErrorMessage"/>
    /// </summary>
    /// <param name="name">The name to include in the formatted string</param>
    /// <returns>A localized string to describe the allowed file content types</returns>
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _formattedTypes);
    }
}
