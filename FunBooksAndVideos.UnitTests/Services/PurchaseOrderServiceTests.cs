using FunBooksAndVideos.Enums;
using FunBooksAndVideos.OrderItems;
using FunBooksAndVideos.OrderProcessing;
using FunBooksAndVideos.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FunBooksAndVideos.UnitTests.Services
{
    public class PurchaseOrderServiceTests
    {
        private readonly PurchaseOrderService _sut;
        private readonly Mock<ItemProcessorFactory> _mockItemProcessorFactory = new();
        private readonly Mock<ProductItemProcessor> _mockProductItemProcessor = new();
        private readonly Mock<IPurchaseItemProcessor> _mockMembershipItemProcessor = new();
        private readonly Mock<ILogger<PurchaseOrderService>> _mockLogger = new();

        public PurchaseOrderServiceTests()
        {
            _sut = new PurchaseOrderService(_mockItemProcessorFactory.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenProcessOrderItem_WhenProcessOrderCalled_ThenAllItemsProcessed()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrderItem
            {
                Id = 1,
                CustomerId = 1,
                TotalPrice = 100,
                PurchaseItems = new List<IPurchaseItem>
                {
                    new ProductItem
                    {
                        Id = 1,
                        Name = "Product 1",
                        Price = 50,
                        ProductType = Product.Type.Book
                    },
                    new MembershipItem
                    {
                        Id = 2,
                        Name = "Membership 1",
                        Price = 50,
                        MembershipType = Membership.Type.BookClub
                    }
                }
            };

            _mockItemProcessorFactory.Setup(f => f.GetPurchaseItemProcessor(It.IsAny<ProductItem>()))
                                     .Returns(_mockProductItemProcessor.Object);
            _mockItemProcessorFactory.Setup(f => f.GetPurchaseItemProcessor(It.IsAny<MembershipItem>()))
                                     .Returns(_mockMembershipItemProcessor.Object);

            // Act
            await _sut.ProcessOrder(purchaseOrder);

            // Assert
            _mockProductItemProcessor.Verify(p => p.Process(It.IsAny<ProductItem>(), purchaseOrder.CustomerId), Times.Once);
            _mockMembershipItemProcessor.Verify(p => p.Process(It.IsAny<MembershipItem>(), purchaseOrder.CustomerId), Times.Once);
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == $"Processing PurchaseOrder request {purchaseOrder.Id} for customer {purchaseOrder.CustomerId}"),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
                Times.Once);
        }
    }
}