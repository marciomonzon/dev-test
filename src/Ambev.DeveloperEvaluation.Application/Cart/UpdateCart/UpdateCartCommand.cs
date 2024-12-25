using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart
{
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public List<Guid> Products { get; private set; } = new List<Guid>();
    }
}
