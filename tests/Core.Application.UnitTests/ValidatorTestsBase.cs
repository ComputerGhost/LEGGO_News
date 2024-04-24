using FluentValidation.Results;

namespace Core.Application.UnitTests;
public class ValidatorTestsBase
{
    protected Stream CreateGoodImageStream()
    {
        const string pngData_base64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVQYV2NgYAAAAAMAAWgmWQ0AAAAASUVORK5CYII=";
        var pngData = Convert.FromBase64String(pngData_base64);
        return new MemoryStream(pngData);
    }

    protected bool HasErrorForProperty(ValidationResult result, params string[] propertyNames)
    {
        var chainedPropertiesName = string.Join('.', propertyNames);
        return result.Errors.Any(e => e.PropertyName == chainedPropertiesName);
    }
}
