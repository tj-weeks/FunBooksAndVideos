using FunBooksAndVideos.Classes;
using FunBooksAndVideos.Events;

namespace FunBooksAndVideos.OrderProcessing
{
    public class MembershipItemProcessor : IPurchaseItemProcessor
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<MembershipItemProcessor> _logger;

        public MembershipItemProcessor(
            IEventBus eventBus,
            ILogger<MembershipItemProcessor> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Process(IPurchaseItem item, int customerId)
        {
            try
            {
                _logger.LogInformation($"Processing Membership request {item.Id} for customer {customerId}.");

                var membershipItem = item as MembershipItem;
                if (membershipItem != null)
                {
                    var activateMembershipEvent = new ActivateMembershipEvent
                    {
                        Item = membershipItem,
                        CustomerId = customerId
                    };

                    await _eventBus.Publish(activateMembershipEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing Membership item {item.Id} for customer {customerId}.");
            }
        }
    }
}
