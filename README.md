# <span>ASP.</span>NET Core File Validation Helpers

Data validation attributes for file uploads in ASP.NET Core applications.

This library offers basic validation of file uploads with IFormFile or IFormFileCollection.
Using one of the provided data validation attributes, the file size, file extension, and content
type can be validated.

This library does not perform advanced file validation. The validation attributes only use the
metadata available in IFormFile and do not read the contents of the file. This type of validation
will not work against malicious users who tamper with the files or request headers.

This class library was created with the .NET CLI command:

```
dotnet new classlib --framework net6.0
dotnet new globaljson --sdk-version 6.0.0
dotnet new editorconfig
dotnet new gitignore
```

## Installation

`dotnet add package Cmg.AspNetCore.FileValidation -s "https://nuget.pkg.github.com/cmg2146/index.json"`

## Usage

The library currently offers 3 validation attributes:

1. MaxFileSizeAttribute
2. ValidFileExtensionsAttribute
3. ValidFileContentTypesAttribute

Use them like the built-in data annotations described [here](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-7.0#validation-attributes), for example:

```csharp
//file UploadImageModel.cs
public class UploadImageModel
{
    [Required]
    [MaxFileSize(MyConstants.MaxImageUploadBytes)]
    [ValidFileExtensions(".jpg", ".png")]
    [ValidFileContentTypes("image/jpeg", "image/png")]
    public IFormFile File { get; set; }
}
```

or

```csharp
//file UsersController.cs
public async Task UpdatePhotoAsync(
    [MaxFileSize(MyConstants.MaxImageUploadBytes)]
    IFormFile photo
)
{
    ...
}
```
