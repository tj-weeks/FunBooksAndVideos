using FunBooksAndVideos.Enums;
using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderItems;
using FunBooksAndVideos.OrderProcessing;
using Microsoft.Extensions.Logging;
using Moq;

namespace FunBooksAndVideos.UnitTests.OrderProcessing
{
    public class ItemProcessorFactoryTests
    {
        private readonly ItemProcessorFactory _sut;
        private readonly Mock<IEventBus> _mockEventBus = new();
        private readonly Mock<ILoggerFactory> _mockLoggerFactory = new();
        private readonly Mock<ILogger<ItemProcessorFactory>> _mockLogger = new();

        public ItemProcessorFactoryTests()
        {
            _mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_mockLogger.Object);

            _sut = new ItemProcessorFactory(_mockEventBus.Object, _mockLoggerFactory.Object);
        }

        [Fact]
        public void GivenAMembershipItem_WhenGetPurchaseItemProcessorCalled_ThenMembershipItemProcessorReturned()
        {
            // Arrange
            var productItem = new MembershipItem
            {
                MembershipType = Membership.Type.BookClub
            };

            // Act
            var result = _sut.GetPurchaseItemProcessor(productItem);

            // Assert
            Assert.IsType<MembershipItemProcessor>(result);
        }

        [Fact]
        public void GivenAProductItem_WhenGetPurchaseItemProcessorCalled_ThenProductItemProcessorReturned()
        {
            // Arrange
            var productItem = new ProductItem
            {
                ProductType = Product.Type.Book
            };

            // Act
            var result = _sut.GetPurchaseItemProcessor(productItem);

            // Assert
            Assert.IsType<ProductItemProcessor>(result);
        }

        [Fact]
        public void GivenAnInvalidItem_WhenGetPurchaseItemProcessorCalled_ThenExceptionRaised()
        {
            // Arrange
            var invalidItem = new InvalidPurchaseItem
            {
                Name = "Invalid"
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.GetPurchaseItemProcessor(invalidItem));
        }

        public class InvalidPurchaseItem : IPurchaseItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }
}