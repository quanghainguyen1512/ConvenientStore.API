using System;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Services;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.DateOfBirth).LessThan(DateTime.Today).WithMessage("The date of birth must be less than current date");
            RuleFor(c => c.FirstName).NotNull();
            RuleFor(c => c.LastName).NotNull();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.PhoneNumber)
                .MinimumLength(9)
                .MaximumLength(12)
                .Matches("[0-9]+");
        }
    }
}