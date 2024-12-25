using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartCommand, Domain.Entities.Cart>();
            CreateMap<Domain.Entities.Cart, UpdateCartResult>();
        }
    }
}
