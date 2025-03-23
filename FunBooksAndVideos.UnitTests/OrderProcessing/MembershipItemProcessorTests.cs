using FunBooksAndVideos.Classes;
using FunBooksAndVideos.Enums;
using FunBooksAndVideos.OrderProcessing;
using Moq;

namespace FunBooksAndVideos.UnitTests.OrderProcessing
{
    public class MembershipItemProcessorTests
    {
        private readonly MembershipItemProcessor _membershipItemProcessor;

        public MembershipItemProcessorTests()
        {
        }

        [Fact]
        public async Task GivenAMembershipItem_WhenProcessCalled_Then()
        {
            // Arrange
            var productItem = new MembershipItem
            {
                Id = 1,
                MembershipType = Membership.Type.Premium,
                Name = "Book",
                Price = 10
            };

            var customerId = 123;

            // Act
            await _membershipItemProcessor.Process(productItem, customerId);

            // Assert
        }
    }
}