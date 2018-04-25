using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class StaffForOperationsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; } // 1: male - 0: female
        public string PhoneNumber { get; set; }

        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new StaffValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}