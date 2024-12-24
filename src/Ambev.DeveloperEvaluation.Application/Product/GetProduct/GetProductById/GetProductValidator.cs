using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct.GetProductById
{
    public class GetProductValidator : AbstractValidator<GetProductCommand>
    {
        public GetProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
