using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart.GetCartById
{
    public class GetCartCommand : IRequest<GetCartResult>
    {
        public Guid Id { get; set; }
    }
}
