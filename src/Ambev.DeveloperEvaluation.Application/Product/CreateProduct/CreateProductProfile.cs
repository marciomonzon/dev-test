using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<CreateProductCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, CreateProductResult>();
        }
    }
}
