using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart
{
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public List<Guid> Products { get; private set; } = new List<Guid>();
    }
}
