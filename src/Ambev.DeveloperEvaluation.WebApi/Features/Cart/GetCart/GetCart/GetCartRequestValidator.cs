using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart.GetCart
{
    public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
    {
        public GetCartRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
