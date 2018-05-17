using System;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class BillValidator : AbstractValidator<BillForOperationsDto>
    {
        public BillValidator()
        {
            RuleFor(b => b.StaffId).NotNull().GreaterThan(0);
            // RuleFor(b => b.CustomerId).NotNull().GreaterThan(0);
            RuleFor(b => b.CreatedDateTime).NotNull();
            RuleFor(b => b.BillDetails).NotEmpty();
        }
    }
}