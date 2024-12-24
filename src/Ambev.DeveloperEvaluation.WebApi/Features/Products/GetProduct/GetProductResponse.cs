namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
