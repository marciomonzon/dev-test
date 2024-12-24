using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .NotEmpty().WithMessage("At least one product must be selected.");
        }
    }
}
