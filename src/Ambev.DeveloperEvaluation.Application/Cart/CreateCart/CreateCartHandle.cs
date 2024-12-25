using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart
{
    public class CreateCartHandle : IRequestHandler<CreateCartCommand, CreateCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateCartHandle(ICartRepository cartRepository,
                                 IProductRepository productRepository,
                                 IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateCartCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cart = new Domain.Entities.Cart(command.UserId);
            cart.AddProductsToCart(command.Products);

            var createdProduct = await _cartRepository.CreateAsync(cart, cancellationToken);
            var result = _mapper.Map<CreateCartResult>(createdProduct);

            return result;
        }

        private async Task<List<Domain.Entities.Product>> GetProductsByIdsAsync(List<Guid> productsIds)
        {
            var products = await _productRepository.GetProductsByIdsAsync(productsIds);

            return products != null ? products.ToList() : new List<Domain.Entities.Product>();
        }

        private List<Domain.Entities.Product> GetIdenticalProducts(List<Domain.Entities.Product> products)
        {
            var idsToMatch = products.Select(x => x.Id).ToList();

            var matchedProducts = products
           .Where(p => idsToMatch.Contains(p.Id))
           .ToList();

            return matchedProducts;
        }

        //private decimal ApplyAndGetDiscounts(CreateSaleCommand command)
        //{
        //    var quantity = command.Products.Count;
        //    decimal discount = 0;

        //    foreach (var product in command.Products)
        //    {
        //        if (quantity >= 4 && quantity < 10)
        //        {
        //            discount = 0.10m;
        //        }
        //        else if (quantity >= 10 && quantity <= 20)
        //        {
        //            discount = 0.20m;
        //        }
        //        else
        //        {
        //            discount = 0.0m;
        //        }
        //    }

        //    return discount;
        //}
    }
}
