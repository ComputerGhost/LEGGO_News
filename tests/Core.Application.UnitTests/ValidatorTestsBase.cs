using FluentValidation.Results;

namespace Core.Application.UnitTests;
public class ValidatorTestsBase : HandlerTestsBase
{
    protected bool HasErrorForProperty(ValidationResult result, params string[] propertyNames)
    {
        var chainedPropertiesName = string.Join('.', propertyNames);
        return result.Errors.Any(e => e.PropertyName == chainedPropertiesName);
    }
}
