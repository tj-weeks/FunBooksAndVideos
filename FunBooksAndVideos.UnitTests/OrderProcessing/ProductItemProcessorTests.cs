using FunBooksAndVideos.Enums;
using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderItems;
using FunBooksAndVideos.OrderProcessing;
using Microsoft.Extensions.Logging;
using Moq;

namespace FunBooksAndVideos.UnitTests.OrderProcessing
{
    public class ProductItemProcessorTests
    {
        private readonly ProductItemProcessor _sut;
        private readonly Mock<IEventBus> _mockEventBus = new();
        private readonly Mock<ILogger<ProductItemProcessor>> _mockLogger = new();

        public ProductItemProcessorTests()
        {
            _sut = new ProductItemProcessor(_mockEventBus.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenAProductItem_WhenProcessCalled_ThenGenerateShippingSlipEventRaised()
        {
            // Arrange
            var productItem = new ProductItem
            {
                Id = 1,
                ProductType = Product.Type.Book,
                Name = "Book",
                Price = 10
            };

            var customerId = 123;

            // Act
            await _sut.Process(productItem, customerId);

            // Assert
            _mockEventBus.Verify(x => x.Publish(It.IsAny<GenerateShippingSlipEvent>()), Times.Once);
        }
    }
}