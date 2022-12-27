using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Base data validation attribute for IFormFile and IFormFileCollection types
/// </summary>
public abstract class FileValidationAttributeBase : ValidationAttribute
{
    /// <summary>
    /// Gets error message
    /// </summary>
    /// <param name="file">The file being validated.</param>
    protected abstract string GetErrorMessage(IFormFile file);
    /// <summary>
    /// Gets error message
    /// </summary>
    /// <param name="files">The file collection being validated.</param>
    protected abstract string GetErrorMessage(IFormFileCollection files);
    /// <summary>
    /// Checks if file is valid
    /// </summary>
    /// <param name="file">The file to validate.</param>
    protected abstract bool IsValid(IFormFile file);

    /// <inheritdoc />
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        else if (value is IFormFile)
        {
            return IsValid((IFormFile)value, validationContext);
        }
        else if (value is IFormFileCollection)
        {
            return IsValid((IFormFileCollection)value, validationContext);
        }
        else
        {
            return new ValidationResult($"Item to validate must be of type {nameof(IFormFile)} or {nameof(IFormFileCollection)}");
        }
    }

    private ValidationResult? IsValid(
        IFormFile file,
        ValidationContext validationContext)
    {
        if (!IsValid(file))
        {
            return new ValidationResult(GetErrorMessage(file));
        }

        return ValidationResult.Success;
    }

    private ValidationResult? IsValid(
        IFormFileCollection files,
        ValidationContext validationContext)
    {
        if (files.Any(file => !IsValid(file)))
        {
            return new ValidationResult(GetErrorMessage(files));
        }

        return ValidationResult.Success;
    }
}
