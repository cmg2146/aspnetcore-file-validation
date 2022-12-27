using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Cmg.AspNetCore.FileValidation;

/// <summary>
/// Base data validation attribute for IFormFile and IFormFileCollection types
/// </summary>
public abstract class FileValidationAttributeBase : ValidationAttribute
{
    /// <inheritdoc />
    protected FileValidationAttributeBase(Func<string> errorMessageAccessor)
        : base(errorMessageAccessor)
    {

    }

    /// <summary>
    /// Checks if file is valid
    /// </summary>
    /// <param name="file">The file to validate.</param>
    protected abstract bool IsValid(IFormFile file);

    /// <summary>
    /// Checks if all files are valid
    /// </summary>
    /// <param name="files">The files to validate.</param>
    protected virtual bool IsValid(IFormFileCollection files)
    {
        return files.All(file => IsValid(file));
    }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }
        else if (value is IFormFile file)
        {
            return IsValid(file);
        }
        else if (value is IFormFileCollection files)
        {
            return IsValid(files);
        }
        else
        {
            throw new ArgumentException("Value must be an IFormFile or IFormFileCollection type", nameof(value));
        }
    }
}
