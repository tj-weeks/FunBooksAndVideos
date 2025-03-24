using FunBooksAndVideos.Enums;
using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderItems;
using FunBooksAndVideos.OrderProcessing;
using Microsoft.Extensions.Logging;
using Moq;

namespace FunBooksAndVideos.UnitTests.OrderProcessing
{
    public class MembershipItemProcessorTests
    {
        private readonly MembershipItemProcessor _sut;
        private readonly Mock<IEventBus> _mockEventBus = new();
        private readonly Mock<ILogger<MembershipItemProcessor>> _mockLogger = new();

        public MembershipItemProcessorTests()
        {
            _sut = new MembershipItemProcessor(_mockEventBus.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenAMembershipItem_WhenProcessCalled_ThenActivateMembershipEventRaised()
        {
            // Arrange
            var membershipItem = new MembershipItem
            {
                Id = 1,
                MembershipType = Membership.Type.Premium,
                Name = "Book",
                Price = 10
            };

            var customerId = 123;

            // Act
            await _sut.Process(membershipItem, customerId);

            // Assert
            _mockEventBus.Verify(x => x.Publish(It.IsAny<ActivateMembershipEvent>()), Times.Once);
        }
    }
}