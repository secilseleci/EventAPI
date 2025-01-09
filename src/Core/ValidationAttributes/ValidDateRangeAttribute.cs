using Core.DTOs.Event;
using System.ComponentModel.DataAnnotations;

public class ValidDateRangeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not CreateEventDto dto)
        {
            return new ValidationResult("Invalid object type.");
        }

        if (dto.StartDate >= dto.EndDate)
        {
            return new ValidationResult("Start date must be earlier than end date.");
        }

        return ValidationResult.Success!;
    }
}
