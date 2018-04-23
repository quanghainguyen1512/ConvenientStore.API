using ConvenientShop.API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Validators
{
    public class AccountValidator : AbstractValidator<AccountForOperationsDto>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Username).MinimumLength(8);
            RuleFor(a => a.Password).MinimumLength(8);
        }
    }
}
