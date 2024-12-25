using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.DeleteCart
{
    public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
    {
        public DeleteCartRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
