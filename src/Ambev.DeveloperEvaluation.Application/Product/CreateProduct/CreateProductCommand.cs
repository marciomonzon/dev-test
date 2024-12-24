using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
