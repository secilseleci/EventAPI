using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValidationAttributes
{
    public class MinLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public MinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue && stringValue.Length < _minLength)
            {
                return new ValidationResult($"The field must be at least {_minLength} characters long.");
            }

            return ValidationResult.Success;
        }
    }

}
