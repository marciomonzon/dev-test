using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime DataOfSale { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public List<Product> Products { get; private set; } = new List<Product>();
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public Sale(string customerName,
                    List<Product> products,
                    decimal discount,
                    decimal totalAmount)
        {
            DataOfSale = DateTime.Now;
            CustomerName = customerName;
            Products = products;
            Discount = discount;
            TotalAmount = totalAmount;
        }

      
    }
}
