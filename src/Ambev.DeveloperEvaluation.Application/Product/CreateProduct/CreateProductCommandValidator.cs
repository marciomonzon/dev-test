using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    public class CreateProductCommandValidator  : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title)
                   .NotEmpty().WithMessage("Title is required.")
                   .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image is required.")
                .Matches(@"^https?://[^\s/$.?#].[^\s]*$").WithMessage("Image URL must be a valid URL.");
        }
    }
}
