using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ItemProcessorFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<ItemProcessorFactory> _logger;

        public ItemProcessorFactory(
            ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<ItemProcessorFactory>();
        }

        public IPurchaseItemProcessor GetPurchaseItemProcessor(IPurchaseItem item)
        {
            _logger.LogInformation($"Selecting Item Processor for purchase item {item}");
            switch (item)
            {
                case ProductItem productItem:
                    return new ProductItemProcessor(_loggerFactory.CreateLogger<ProductItemProcessor>());
                case MembershipItem membershipItem:
                    return new MembershipItemProcessor(_loggerFactory.CreateLogger<MembershipItemProcessor>());
                default:
                    throw new Exception($"Unknown purchase item {item}.");
            }
        }
    }
}
