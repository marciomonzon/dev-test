using Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartRequestValidator()
        {
            RuleFor(x => x.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .NotEmpty().WithMessage("At least one product must be selected.");
        }
    }
}
