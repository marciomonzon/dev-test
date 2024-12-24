using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, UpdateProductResult>();
        }
    }

}
