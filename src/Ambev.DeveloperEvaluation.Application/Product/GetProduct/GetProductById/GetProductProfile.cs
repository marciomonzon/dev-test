using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct.GetProductById
{
    public class GetProductProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetUser operation
        /// </summary>
        public GetProductProfile()
        {
            CreateMap<Domain.Entities.Product, GetProductResult>();
        }
    }
}
