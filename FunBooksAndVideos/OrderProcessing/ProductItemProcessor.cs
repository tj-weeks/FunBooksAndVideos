using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderItems;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ProductItemProcessor : IPurchaseItemProcessor
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ProductItemProcessor> _logger;

        public ProductItemProcessor(
            IEventBus eventBus,
            ILogger<ProductItemProcessor> logger)
        {
            _eventBus = eventBus;
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

                    await _eventBus.Publish(generateShippingSlipEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing product item {item.Id} for customer {customerId}.");
            }
        }
    }
}