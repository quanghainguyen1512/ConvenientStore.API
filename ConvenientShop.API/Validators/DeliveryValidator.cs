using System;
using ConvenientShop.API.Models;
using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class DeliveryValidator : AbstractValidator<DeliveryForOperationsDto>
    {
        public DeliveryValidator()
        {
            RuleFor(d => d.Cost).NotNull().GreaterThan(1000);
            RuleFor(d => d.DeliveryDate).NotEqual(new DateTime()).WithMessage("Delivery Time should be set, not the initial value");
            RuleFor(d => d.Shipments).NotEmpty();
        }
    }
}