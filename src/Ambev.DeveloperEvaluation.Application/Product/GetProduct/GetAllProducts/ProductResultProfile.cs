using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct.GetAllProducts
{
    public class ProductResultProfile : Profile
    {
        public ProductResultProfile()
        {
            CreateMap<Domain.Entities.Product, GetAllProductsResult>();
        }
    }
}
