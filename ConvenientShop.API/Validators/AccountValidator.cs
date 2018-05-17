using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class AccountValidator : AbstractValidator<AccountForOperationsDto>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Username).MinimumLength(8).Matches("[A-Za-z0-9").WithMessage("The username is invalid, only accecpt words and numbers");
            RuleFor(a => a.Password).MinimumLength(8).Matches("[A-Za-z0-9").WithMessage("The password is invalid, only accecpt words and numbers");
        }
    }
}