using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart
{
    public class UpdateCartHandle : IRequestHandler<UpdateCartCommand, UpdateCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateCartHandle(ICartRepository cartRepository,
                                IProductRepository productRepository,
                                IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cart = await _cartRepository.GetByIdAsync(command.Id);

            cart.AddProductsToCart(command.Products);

            var createdProduct = await _cartRepository.UpdateAsync(cart, cancellationToken);
            var result = _mapper.Map<UpdateCartResult>(createdProduct);

            return result;
        }
    }
}
