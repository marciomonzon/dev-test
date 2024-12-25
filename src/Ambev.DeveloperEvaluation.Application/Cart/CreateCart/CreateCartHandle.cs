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

            cart.TotalAmount = await GetCalculatedTotalAmountAsync(command.Products);

            cart.Discount = ApplyDiscountIdenticalitems(command.Products);

            cart.ApplyDiscount();

            var createdProduct = await _cartRepository.CreateAsync(cart, cancellationToken);
            var result = _mapper.Map<CreateCartResult>(createdProduct);

            return result;
        }

        private async Task<decimal> GetCalculatedTotalAmountAsync(List<Guid> productsIds)
        {
            var products = await GetProductsByIdsAsync(productsIds);
            decimal totalAmount = 0;

            foreach (var product in products)
            {
                totalAmount += product.Price;
            }

            return totalAmount;
        }

        private async Task<List<Domain.Entities.Product>> GetProductsByIdsAsync(List<Guid> productsIds)
        {
            var products = await _productRepository.GetProductsByIdsAsync(productsIds);

            return products != null ? products.ToList() : new List<Domain.Entities.Product>();
        }

        private decimal ApplyDiscountIdenticalitems(List<Guid> products)
        {
            var identicalItems = GetIdenticalProducts(products);
            var quantity = identicalItems.Count();
            decimal discount = 0;

            if (quantity > 20)
                throw new Exception("It's not possible to sell above 20 identical items");

            if (quantity >= 4 && quantity < 10)
                discount = 0.10m;
            else if (quantity >= 10 && quantity <= 20)
                discount = 0.20m;
            else
                discount = 0.0m;

            return discount;
        }

        private List<Guid> GetIdenticalProducts(List<Guid> idsProductToMatch)
        {
            var identicalItems = idsProductToMatch
                                 .GroupBy(g => g)
                                 .Where(g => g.Count() > 1)
                                 .Select(g => g.Key)
                                 .ToList();

            return identicalItems;
        }
    }
}
