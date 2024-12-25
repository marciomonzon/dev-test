using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart.GetAllCarts
{
    internal class GetAllCartsHandle : IRequestHandler<GetAllCartsCommand, CartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetAllCartsHandle(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartResult> Handle(GetAllCartsCommand request, CancellationToken cancellationToken)
        {
            var carts = await _cartRepository.GetAllAsync(cancellationToken);

            var cartsResult = _mapper.Map<List<GetAllCartsResult>>(carts);

            var result = new CartResult();

            result.Carts.AddRange(cartsResult);

            return result;
        }
    }
}
