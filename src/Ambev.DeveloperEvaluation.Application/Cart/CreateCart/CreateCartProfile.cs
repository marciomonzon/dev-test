using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<CreateCartCommand, Domain.Entities.Cart>();
            CreateMap<Domain.Entities.Cart, CreateCartResult>();
        }
    }
}
