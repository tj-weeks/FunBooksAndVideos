using FunBooksAndVideos.OrderProcessing;

namespace FunBooksAndVideos.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ItemProcessorFactory _itemProcessorFactory;
        private readonly ILogger<PurchaseOrderService> _logger;

        public PurchaseOrderService(
            ItemProcessorFactory itemProcessorFactory,
            ILogger<PurchaseOrderService> logger)
        {
            _itemProcessorFactory = itemProcessorFactory;
            _logger = logger;
        }

        public async Task ProcessOrder(OrderItems.PurchaseOrderItem order)
        {
            _logger.LogInformation($"Processing PurchaseOrder request {order.Id} for customer {order.CustomerId}");

            foreach (var item in order.PurchaseItems)
            {
                var purchaseItemProcessor = _itemProcessorFactory.GetPurchaseItemProcessor(item);
                await purchaseItemProcessor.Process(item, order.CustomerId);
            }
        }
    }
}
