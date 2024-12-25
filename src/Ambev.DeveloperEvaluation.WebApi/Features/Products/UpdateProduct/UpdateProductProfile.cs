using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductResult, UpdateProductResponse>();
        }
    }
}
