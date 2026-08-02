namespace AvaloniaFramework.Presentation.View.Validation;

public interface ValueValidator
{
    (bool IsValid, string ErrorMessage) Validate(object value);
}