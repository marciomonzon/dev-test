using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DataOfSale { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<string> Products { get; set; } = new List<string>();
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
