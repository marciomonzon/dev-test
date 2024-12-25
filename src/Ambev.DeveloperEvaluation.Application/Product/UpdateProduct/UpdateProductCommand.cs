using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResult>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
