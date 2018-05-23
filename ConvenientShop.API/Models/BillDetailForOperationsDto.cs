using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConvenientShop.API.Validators;

namespace ConvenientShop.API.Models
{
    public class BillDetailForOperationsDto
    {
        public string BarCode { get; set; }
        public int Quantity { get; set; }

        public(bool isValid, IEnumerable<ValidationResult> errors) Validate(int index)
        {
            var validator = new BillDetailValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return (true, null);

            return (false, result.Errors.Select(item => new ValidationResult($"At index {index}: {item.ErrorMessage}", new [] { item.PropertyName })));
        }
    }
}