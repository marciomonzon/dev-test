namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public int SaleNumberId { get; set; }
        public DateTime DataOfSale { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<string> Products { get; set; } = new List<string>();
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
