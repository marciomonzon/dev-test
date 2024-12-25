using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.DeleteCart
{
    public class DeleteCartHandle : IRequestHandler<DeleteCartCommand, DeleteCartResult>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteCartHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<DeleteCartResult> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
        {
            var product = await _cartRepository.GetByIdAsync(command.Id);
            var result = new DeleteCartResult();

            if (product == null)
            {
                result.Result = false;
                return result;
            }

            result.Result = await _cartRepository.DeleteAsync(product.Id, cancellationToken);
            return result;
        }
    }
}
