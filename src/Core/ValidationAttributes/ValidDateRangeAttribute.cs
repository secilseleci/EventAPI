using System.ComponentModel.DataAnnotations;

public class ValidDateRangeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Value cannot be null.");
        }

        var startDateProperty = validationContext.ObjectType.GetProperty("StartDate");
        var endDateProperty = validationContext.ObjectType.GetProperty("EndDate");

        if (startDateProperty == null || endDateProperty == null)
        {
            return new ValidationResult("Object must have StartDate and EndDate properties.");
        }

        var startDate = startDateProperty.GetValue(validationContext.ObjectInstance) as DateTimeOffset?;
        var endDate = endDateProperty.GetValue(validationContext.ObjectInstance) as DateTimeOffset?;

        if (startDate == null || endDate == null)
        {
            return new ValidationResult("Start date and end date cannot be null.");
        }

        if (startDate >= endDate)
        {
            return new ValidationResult("Start date must be earlier than end date.");
        }

        return ValidationResult.Success!;
    }
}
