using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.DeleteCart
{
    public class DeleteCartCommand : IRequest<DeleteCartResult>
    {
        public Guid Id { get; set; }
    }
}
