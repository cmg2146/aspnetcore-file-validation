# <span>ASP.</span>NET Core File Validation Helpers

Data validation attributes for file uploads in ASP.NET Core applications.

This library offers basic validation of file uploads with IFormFile or IFormFileCollection.
Using one of the provided data validation attributes, the file size, file extension, and content
type can be validated.

The provided validation attributes do not read the file and instead rely on the metadata available
in IFormFile. This library is not cabable of performing advanced file validation.

This class library was created with the .NET CLI command:

```
dotnet new classlib --framework net6.0
dotnet new globaljson --sdk-version 6.0.0
dotnet new editorconfig
dotnet new gitignore
```

## Installation

TODO: Install the package from Nuget:

```dotnet add package Cmg.AspNetCore.FileValidation```
