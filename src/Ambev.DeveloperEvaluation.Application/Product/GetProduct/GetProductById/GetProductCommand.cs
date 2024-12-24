using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct.GetProductById
{
    public class GetProductCommand : IRequest<GetProductResult>
    {
        public Guid Id { get; set; }
    }
}
