using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class ShipmentValidator : AbstractValidator<ShipmentForOperationsDto>
    {
        public ShipmentValidator()
        {
            RuleFor(s => s.ProductDetail).NotNull();
        }
    }
}