using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart.GetCartById
{
    public class GetCartByIdProfile : Profile
    {
        public GetCartByIdProfile()
        {
            CreateMap<Domain.Entities.Cart, GetCartResult>();
        }
    }
}
