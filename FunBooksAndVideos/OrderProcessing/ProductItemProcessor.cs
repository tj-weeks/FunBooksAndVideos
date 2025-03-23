using FunBooksAndVideos.Classes;
using FunBooksAndVideos.Events;
using FunBooksAndVideos.PurchaseOrder;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ProductItemProcessor : IPurchaseItemProcessor
    {
        private readonly IMessageSession _endpointInstance;
        private readonly ILogger<ProductItemProcessor> _logger;

        public ProductItemProcessor(
            IMessageSession endpointInstance,
            ILogger<ProductItemProcessor> logger)
        {
            _endpointInstance = endpointInstance;
            _logger = logger;
        }

        public async Task Process(IPurchaseItem item, int customerId)
        {
            try
            {
                _logger.LogInformation($"Processing Product request {item.Id} for customer {customerId}.");

                var productItem = item as ProductItem;
                if (productItem != null)
                {
                    var generateShippingSlipEvent = new GenerateShippingSlipEvent
                    {
                        Item = productItem,
                        CustomerId = customerId
                    };

                    await _endpointInstance.Publish(generateShippingSlipEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing product item {item.Id} for customer {customerId}.");
            }
        }
    }
}