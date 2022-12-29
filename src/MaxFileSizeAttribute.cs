using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Data validation attribute for validating file size of an IFormFile or IFormFileCollection
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class MaxFileSizeAttribute : FileValidationAttribute
{
    /// <summary>
    /// Max file size data validation attribute constructor
    /// </summary>
    /// <param name="maxFileSizeBytes">Max valid file size in bytes.</param>
    public MaxFileSizeAttribute(long maxFileSizeBytes)
        : base(() => DefaultErrorMessageString)
    {
        MaxFileSizeBytes = maxFileSizeBytes;
    }

    /// <summary>
    /// Max file size in bytes.
    /// </summary>
    public long MaxFileSizeBytes { get; }

    private static readonly string DefaultErrorMessageString =
        "Max file size exceeded in '{0}' field. Max allowed file size is '{1}' bytes.";

    /// <summary>
    /// Checks if a file is within the size limit
    /// </summary>
    protected override bool IsValid(IFormFile file)
    {
        return file.Length <= MaxFileSizeBytes;
    }

    /// <summary>
    /// Override of <see cref="ValidationAttribute.FormatErrorMessage"/>
    /// </summary>
    /// <param name="name">The name to include in the formatted string</param>
    /// <returns>A localized string to describe the max file size</returns>
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MaxFileSizeBytes);
    }
}
