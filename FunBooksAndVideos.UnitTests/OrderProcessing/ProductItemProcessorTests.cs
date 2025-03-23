using FunBooksAndVideos.Classes;
using FunBooksAndVideos.Enums;
using FunBooksAndVideos.OrderProcessing;

namespace FunBooksAndVideos.UnitTests.OrderProcessing
{
    public class ProductItemProcessorTests
    {
        private readonly ProductItemProcessor _productItemProcessor;

        public ProductItemProcessorTests()
        {
        }

        [Fact]
        public async Task GivenAProductItem_WhenProcessCalled_ThenSlippingSlipGenerated()
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
            await _productItemProcessor.Process(productItem, customerId);

            // Assert
        }
    }
}