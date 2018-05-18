using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailForOperationsDto>
    {
        public OrderDetailValidator()
        {
            RuleFor(od => od.ProductQuantity).NotNull().GreaterThan(0);
        }
    }
}