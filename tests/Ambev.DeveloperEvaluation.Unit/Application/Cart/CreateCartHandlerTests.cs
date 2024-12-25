using Ambev.DeveloperEvaluation.Application.Cart.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart
{
    public class CreateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly CreateCartHandle _handler;

        public CreateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateCartHandle(_cartRepository, _productRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnCreateCartResult()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Products = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };
            var products = command.Products.Select(id => new Ambev.DeveloperEvaluation.Domain.Entities.Product { Id = id, Price = 50 }).ToList();
            var cart = new Ambev.DeveloperEvaluation.Domain.Entities.Cart(command.UserId)
            {
                TotalAmount = 100,
                Discount = 0.10m
            };

            _productRepository.GetProductsByIdsAsync(command.Products)
                .Returns(products);

            _cartRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(cart));

            var expectedResult = new CreateCartResult { };
            _mapper.Map<CreateCartResult>(cart).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
            await _productRepository.Received(1).GetProductsByIdsAsync(command.Products);
            await _cartRepository.Received(1).CreateAsync(Arg.Is<DeveloperEvaluation.Domain.Entities.Cart>(c => c.UserId == command.UserId && c.TotalAmount == 100), Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<CreateCartResult>(cart);
        }

        [Fact]
        public async Task Handle_ShouldCalculateTotalAmountCorrectly()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Products = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            var products = new List<DeveloperEvaluation.Domain.Entities.Product>
        {
            new DeveloperEvaluation.Domain.Entities.Product { Id = command.Products[0], Price = 100m },
            new DeveloperEvaluation.Domain.Entities.Product { Id = command.Products[1], Price = 200m }
        };

            _productRepository.GetProductsByIdsAsync(Arg.Any<List<Guid>>())
                .Returns(products);

            _cartRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new DeveloperEvaluation.Domain.Entities.Cart(command.UserId)));

            _mapper.Map<CreateCartResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>()).Returns(new CreateCartResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _productRepository.Received(1).GetProductsByIdsAsync(command.Products);
            _cartRepository.Received(1).CreateAsync(Arg.Is<DeveloperEvaluation.Domain.Entities.Cart>(c => c.TotalAmount == 300m), Arg.Any<CancellationToken>());
        }

        [Fact]
        public void Handle_ShouldThrowExceptionWhenMoreThan20IdenticalItems()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Products = Enumerable.Repeat(Guid.NewGuid(), 21).ToList()
            };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<Exception>().WithMessage("It's not possible to sell above 20 identical items");
        }

        [Theory]
        [InlineData(0.20, 20)]
        [InlineData(0.10, 9)]
        public async Task Handle_ShouldApplyDiscountCorrectlyForIdenticalItems(decimal discount, int quantity)
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productId2 = Guid.NewGuid();
            var command = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Products = Enumerable.Repeat(Guid.NewGuid(), quantity).ToList()
            };

            var products = Enumerable.Repeat(new DeveloperEvaluation.Domain.Entities.Product { Id = productId, Price = 100m }, 10).ToList();

            _productRepository.GetProductsByIdsAsync(Arg.Any<List<Guid>>())
                .Returns(products);

            DeveloperEvaluation.Domain.Entities.Cart capturedCart = null;
            _cartRepository.CreateAsync(Arg.Do<DeveloperEvaluation.Domain.Entities.Cart>(cart => capturedCart = cart), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new DeveloperEvaluation.Domain.Entities.Cart(command.UserId)));

            _mapper.Map<CreateCartResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>()).Returns(new CreateCartResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            capturedCart.Should().NotBeNull();
            capturedCart.Discount.Should().Be(discount);
            await _cartRepository.Received(1).CreateAsync(capturedCart, Arg.Any<CancellationToken>());
        }
    }
}
