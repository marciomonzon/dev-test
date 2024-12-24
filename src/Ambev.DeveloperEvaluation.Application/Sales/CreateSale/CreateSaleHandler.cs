using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository,
                                 IProductRepository productRepository,
                                 IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var products = await GetProductsByIdsAsync(command.Products);

            var sale = new Sale(command.CustomerName,
                                products,
                                command.Discount,
                                command.TotalAmount);

            var createdUser = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createdUser);

            return result;
        }

        private async Task<List<Domain.Entities.Product>> GetProductsByIdsAsync (List<Guid> productsIds)
        {
            var products = await _productRepository.GetProductsByIdsAsync(productsIds);

            return products != null ? products.ToList() : new List<Domain.Entities.Product>();
        }
    }
}
