using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(x => x.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .NotEmpty().WithMessage("At least one product must be selected.");
        }
    }
}
