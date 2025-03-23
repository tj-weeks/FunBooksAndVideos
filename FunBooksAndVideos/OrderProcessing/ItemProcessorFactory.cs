using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ItemProcessorFactory
    {
        private readonly IMessageSession _endpointInstance;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<ItemProcessorFactory> _logger;

        public ItemProcessorFactory(
            IMessageSession endpointInstance,
            ILoggerFactory loggerFactory)
        {
            _endpointInstance = endpointInstance;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<ItemProcessorFactory>();
        }

        public IPurchaseItemProcessor GetPurchaseItemProcessor(IPurchaseItem item)
        {
            _logger.LogInformation($"Selecting Item Processor for purchase item {item}");
            switch (item)
            {
                case ProductItem productItem:
                    return new ProductItemProcessor(_endpointInstance, _loggerFactory.CreateLogger<ProductItemProcessor>());
                case MembershipItem membershipItem:
                    return new MembershipItemProcessor(_endpointInstance, _loggerFactory.CreateLogger<MembershipItemProcessor>());
                default:
                    throw new Exception($"Unknown purchase item {item}.");
            }
        }
    }
}
