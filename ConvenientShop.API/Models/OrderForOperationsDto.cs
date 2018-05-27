using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class OrderForOperationsDto
    {
        public int StaffId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int SupplierId { get; set; }
        public IEnumerable<OrderDetailForOperationsDto> OrderDetails { get; set; }
        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new OrderValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}