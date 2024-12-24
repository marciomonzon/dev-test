using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, ProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResult> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            var productsResult = _mapper.Map<List<GetAllProductsResult>>(products);

            var result = new ProductResult();

            result.Products.AddRange(productsResult);

            return result;
        }
    }
}
