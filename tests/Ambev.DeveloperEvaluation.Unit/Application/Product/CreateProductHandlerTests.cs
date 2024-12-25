using Ambev.DeveloperEvaluation.Application.Product.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    public class CreateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateProductHandler(_productRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnCreateProductResult()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = "Sample Product",
                Price = 100,
                Description = "Sample Description",
                Category = "Sample Category",
                Image = "http://localhost.com"
            };

            var product = new DeveloperEvaluation.Domain.Entities.Product(command.Title, command.Price, command.Description, command.Category, command.Image);
            var createdProduct = product;

            _productRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>(), Arg.Any<CancellationToken>())
                              .Returns(Task.FromResult(createdProduct));

            var expectedResult = new CreateProductResult { };
            _mapper.Map<CreateProductResult>(createdProduct).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
            await _productRepository.Received(1).CreateAsync(Arg.Is<DeveloperEvaluation.Domain.Entities.Product>(
                p => p.Title == command.Title &&
                     p.Price == command.Price &&
                     p.Description == command.Description &&
                     p.Category == command.Category &&
                     p.Image == command.Image
            ), Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<CreateProductResult>(createdProduct);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = "",
                Price = -1,
                Description = "",
                Category = "",
                Image = ""
            };

            var validator = Substitute.For<CreateProductCommandValidator>();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Title", "Title is required.") });

            validator.ValidateAsync(command, Arg.Any<CancellationToken>()).Returns(Task.FromResult(validationResult));

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Contains("Title is required.", exception.Message);
            await _productRepository.DidNotReceive().CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>(), Arg.Any<CancellationToken>());
            _mapper.DidNotReceive().Map<CreateProductResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>());
        }
    }
}
