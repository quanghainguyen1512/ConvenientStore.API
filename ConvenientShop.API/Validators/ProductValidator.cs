using System;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.Price).NotNull().GreaterThan(1000);
            RuleFor(p => p.Unit).NotNull();
        }
    }
}