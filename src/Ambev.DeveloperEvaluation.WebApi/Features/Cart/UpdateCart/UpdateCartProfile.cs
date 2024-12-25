using Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartRequest, UpdateCartCommand>();
            CreateMap<UpdateCartResult, UpdateCartResponse>();
        }
    }
}
