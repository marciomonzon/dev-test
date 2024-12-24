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

        private const int MinDiscountableQuantity = 4;
        private const int MidDiscountableQuantity = 10;
        private const int MaxDiscountableQuantity = 20;
        private const decimal DiscountLow = 0.10m; 
        private const decimal DiscountHigh = 0.20m;

        public Sale(string customerName,
                    List<Product> products,
                    decimal discount,
                    decimal totalAmount)
        {
            if (products == null || !products.Any())
                throw new ArgumentException("The sale must have at least one product.");

            DataOfSale = DateTime.Now;
            CustomerName = customerName;
            Products = products;
            Discount = discount;
            TotalAmount = totalAmount;


            ApplyDiscounts();
            CalculateTotalAmount();
        }

        private void ApplyDiscounts()
        {
            var productGroups = Products
                .GroupBy(p => p.Id)
                .Select(g => new { ProductId = g.Key, Quantity = g.Count() })
                .ToList();

            foreach (var group in productGroups)
            {
                if (group.Quantity > MaxDiscountableQuantity)
                    throw new InvalidOperationException($"Cannot sell more than {MaxDiscountableQuantity} identical items.");

                if (group.Quantity >= MidDiscountableQuantity)
                    Discount += DiscountHigh;
                else if (group.Quantity >= MinDiscountableQuantity)
                    Discount += DiscountLow;
            }
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = Products.Sum(p => p.Price);

            if (Discount > 0)
            {
                TotalAmount -= TotalAmount * Discount;
            }
        }

    }
}
