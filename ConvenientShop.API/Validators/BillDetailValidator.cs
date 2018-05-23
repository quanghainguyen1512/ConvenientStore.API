using FluentValidation;

namespace ConvenientShop.API.Validators
{
    public class BillDetailValidator : AbstractValidator<Models.BillDetailForOperationsDto>
    {
        public BillDetailValidator()
        {
            RuleFor(bd => bd.BarCode).NotNull().NotEmpty();
            RuleFor(bd => bd.Quantity).NotNull().GreaterThan(0);
        }
    }
}