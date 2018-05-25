using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class DeliveryForOperationsDto
    {
        public DateTime DeliveryDate { get; set; }
        public int Cost { get; set; }
        public IEnumerable<ShipmentForOperationsDto> Shipments { get; set; }
        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new DeliveryValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}