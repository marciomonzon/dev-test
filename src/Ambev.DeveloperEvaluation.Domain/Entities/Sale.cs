using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime DataOfSale { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public List<string> Products { get; private set; } = new List<string>();
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public Sale(string customerName,
                    List<string> products,
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
