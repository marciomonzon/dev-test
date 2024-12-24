using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart.GetAllCarts
{
    public class GetAllCartsProfile : Profile
    {
        public GetAllCartsProfile()
        {
            CreateMap<Domain.Entities.Cart, GetAllCartsResult>();
        }
    }
}
