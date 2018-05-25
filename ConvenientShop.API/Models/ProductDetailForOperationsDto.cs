using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class ProductDetailForOperationsDto
    {
        public string BarCode { get; set; }
        public int QuantityOnStore { get; set; } = 0;
        public int QuantityInRepository { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Price { get; set; }
        public(bool isValid, IEnumerable<ValidationResult> errors) Validate()
        {
            var validator = new ProductDetailValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new [] { item.PropertyName })));
        }
    }
}