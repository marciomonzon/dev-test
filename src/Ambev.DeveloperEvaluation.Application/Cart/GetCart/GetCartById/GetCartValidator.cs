using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart.GetCartById
{
    public class GetCartValidator : AbstractValidator<GetCartCommand>
    {
        public GetCartValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
