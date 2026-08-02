using System.Globalization;

namespace AvaloniaFramework.Presentation.View.Validation;

public class Int32Validator : ValueValidator
{
    private readonly CultureInfo cultureInfo;

    public Int32Validator(CultureInfo cultureInfo = null)
    {
        this.cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;
    }

    public (bool IsValid, string ErrorMessage) Validate(object value)
    {
        switch (value)
        {
            case string strValue:
                return int.TryParse(strValue, NumberStyles.Integer, cultureInfo, out _)
                    ? (true, string.Empty)
                    : (false, GetInvalidValueMessage(strValue));

            case int _:
                return (true, string.Empty);

            case null:
                return (false, GetNullErrorMessage());

            default:
                return (false, GetUnexpectedValueMessage(value?.ToString()));
        }
    }

    private string GetNullErrorMessage()
    {
        return Messages.ResourceManager.GetString(nameof(Messages.ValueValidatorNullErrorMessage), cultureInfo);
    }

    private string GetInvalidValueMessage(string strValue)
    {
        return string.Format(
            cultureInfo,
            Messages.ResourceManager.GetString(nameof(Messages.ValueValidatorInvalidValueMessage), cultureInfo),
            strValue);
    }

    private string GetUnexpectedValueMessage(string strValue)
    {
        return string.Format(
            cultureInfo,
            Messages.ResourceManager.GetString(nameof(Messages.ValueValidatorUnexpectedValueMessage), cultureInfo),
            strValue);
    }
}