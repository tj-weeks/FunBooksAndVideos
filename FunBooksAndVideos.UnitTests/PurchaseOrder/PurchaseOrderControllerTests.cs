using FunBooksAndVideos.Enums;
using FunBooksAndVideos.OrderItems;
using FunBooksAndVideos.PurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace FunBooksAndVideos.UnitTests.PurchaseOrder
{
    public class PurchaseOrderControllerTests
    {
        private readonly PurchaseOrderController _sut;
        private readonly Mock<ILogger<PurchaseOrderController>> _mockLogger = new();
        private readonly Mock<IMediator> _mockMediator = new();

        public PurchaseOrderControllerTests()
        {
            _sut = new PurchaseOrderController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task PostAsync_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var purchaseOrder = new OrderItems.PurchaseOrderItem
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
                        Price = 50
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

            var command = new PurchaseOrderCommand(purchaseOrder);
            var response = new PurchaseOrderResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<PurchaseOrderCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _sut.PostAsync(purchaseOrder);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostAsync_WhenMediatorReturnsError_ReturnsInternalServerError()
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
                        Price = 50
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

            var command = new PurchaseOrderCommand(purchaseOrder);
            var response = new PurchaseOrderResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<PurchaseOrderCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _sut.PostAsync(purchaseOrder);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}