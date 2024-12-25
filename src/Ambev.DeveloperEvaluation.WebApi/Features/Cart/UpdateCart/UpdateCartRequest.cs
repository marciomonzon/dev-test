namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart
{
    public class UpdateCartRequest
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public List<Guid> Products { get; private set; } = new List<Guid>();
    }
}
