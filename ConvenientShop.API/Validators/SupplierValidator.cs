using System;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class SupplierValidator : AbstractValidator<SupplierWithoutProductsDto>
    {
        public SupplierValidator()
        {
            RuleFor(s => s.PhoneNumber)
                .MinimumLength(9)
                .MaximumLength(12)
                .Matches("[^a-z0-9]+")
                .NotNull();
            RuleFor(s => s.Email).EmailAddress();
            RuleFor(s => s.SupplierName).NotNull().NotEmpty();
        }
    }
}