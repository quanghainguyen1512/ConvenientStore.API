using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class ShipmentForOperationsDto
    {
        public OrderDetailForOperationsDto OrderDetail { get; set; }
        public ProductDetailForOperationsDto ProductDetail { get; set; }
        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new ShipmentValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}