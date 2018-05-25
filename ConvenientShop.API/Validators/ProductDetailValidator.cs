using System;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class ProductDetailValidator : AbstractValidator<ProductDetailForOperationsDto>
    {
        public ProductDetailValidator()
        {
            RuleFor(pd => pd.QuantityOnStore).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(pd => pd.QuantityInRepository).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(pd => pd.ExpirationDate).NotNull().GreaterThan(DateTime.Today).WithMessage("The expiration date must be greater than today");
        }
    }
}