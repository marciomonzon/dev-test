using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumberId)
                .NotEmpty().WithMessage("Sale number must be provided.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name must be provided.")
                .MaximumLength(200).WithMessage("Customer name cannot exceed 200 characters.");

            RuleFor(x => x.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .NotEmpty().WithMessage("At least one product must be selected.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.")
                .LessThanOrEqualTo(100).WithMessage("Discount cannot exceed 100.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to 0.");

            RuleFor(x => x)
                .Custom((sale, context) =>
                {
                    if (sale.Discount > sale.TotalAmount)
                    {
                        context.AddFailure("Discount", "Discount cannot be greater than the total amount.");
                    }
                });
        }
    }
}
