using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart
{
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<Guid> Products { get; set; } = new List<Guid>();
    }
}
