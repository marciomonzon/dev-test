using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        public DateTime Date { get; private set; }
        public ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        public Cart()
        {
        }

        public Cart(Guid userId)
        {
            UserId = userId;
            Date = DateTime.Now;
        }

        public void AddProductsToCart(List<Guid> products)
        {
            foreach (var item in products)
            {
                CartProducts.Add(new CartProduct { CartId = Id, ProductId = item });
            }
        }

        public void RemoveAllProducts(List<Guid> products) 
        {
            products = new List<Guid>();
        }

        public void ApplyDiscount()
        {
            TotalAmount = TotalAmount - Discount;
        }
    }
}
