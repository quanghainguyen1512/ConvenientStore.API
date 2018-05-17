using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class AccountForOperationsDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new AccountValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}