using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class BillForOperationsDto
    {
        public int StaffId { get; set; }
        // public int CustomerId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public IEnumerable<BillDetailForOperationsDto> BillDetails { get; set; }

        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new BillValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}