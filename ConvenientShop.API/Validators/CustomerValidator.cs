using System;
using ConvenientShop.API.Entities;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.DateOfBirth).LessThan(DateTime.Today);
            RuleFor(c => c.FirstName).NotNull();
            RuleFor(c => c.LastName).NotNull();
            RuleFor(c => c.Email).EmailAddress();
        }
    }
}