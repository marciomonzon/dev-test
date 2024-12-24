namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public Guid SaleNumberId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<string> Products { get; set; } = new List<string>();
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
