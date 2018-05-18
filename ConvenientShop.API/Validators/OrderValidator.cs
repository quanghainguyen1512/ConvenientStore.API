using System;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class OrderValidator : AbstractValidator<OrderForOperationsDto>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderDateTime).NotNull().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(o => o.OrderDetails).NotEmpty();
        }
    }
}