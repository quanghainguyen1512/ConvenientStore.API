using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class OrderDetailForOperationsDto
    {
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public bool IsReceived { get; set; } = false;
        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new OrderDetailValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}