using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .NotEmpty().WithMessage("At least one product must be selected.");
        }
    }
}
