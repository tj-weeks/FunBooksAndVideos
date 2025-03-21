using FunBooksAndVideos.Classes;
using FunBooksAndVideos.Enums;
using FunBooksAndVideos.OrderProcessing;

namespace FunBooksAndVideos.UnitTests.OrderProcessing
{
    public class ItemProcessorFactoryTests
    {
        [Fact]
        public void GivenAMembershipItem_WhenGetPurchaseItemProcessorCalled_ThenMembershipItemProcessorReturned()
        {
            // Arrange
            var productItem = new MembershipItem
            {
                MembershipType = Membership.Type.BookClub
            };

            // Act
            var result = ItemProcessorFactory.GetPurchaseItemProcessor(productItem);

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
            var result = ItemProcessorFactory.GetPurchaseItemProcessor(productItem);

            // Assert
            Assert.IsType<ProductItemProcessor>(result);
        }
    }
}