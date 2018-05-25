using System;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class ProductValidator : AbstractValidator<ProductWithoutDetailDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.Unit).NotNull();
        }
    }
}