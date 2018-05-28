using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
    {
        public class StaffValidator : AbstractValidator<StaffForOperationsDto>
            {
                public StaffValidator()
                {
                    RuleFor(s => s.FirstName).NotEmpty();
                    RuleFor(s => s.LastName).NotEmpty();
                    RuleFor(s => s.DateOfBirth).LessThan(DateTime.Today).WithMessage("The date of birth must be less than current date");
                    RuleFor(s => s.IdentityNumber).NotEmpty();
                    RuleFor(s => s.PhoneNumber)
                        .MinimumLength(9)
                        .MaximumLength(12)
                        .Matches("[0-9]+[^a-z]")
                        .NotNull();
        }
    }
}