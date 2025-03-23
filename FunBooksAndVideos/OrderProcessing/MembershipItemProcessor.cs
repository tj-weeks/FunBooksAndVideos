using FunBooksAndVideos.Classes;
using FunBooksAndVideos.Events;

namespace FunBooksAndVideos.OrderProcessing
{
    public class MembershipItemProcessor : IPurchaseItemProcessor
    {
        private readonly IMessageSession _endpointInstance;
        private readonly ILogger<MembershipItemProcessor> _logger;

        public MembershipItemProcessor(
            IMessageSession endpointInstance,
            ILogger<MembershipItemProcessor> logger)
        {
            _endpointInstance = endpointInstance;
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

                    await _endpointInstance.Publish(activateMembershipEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing Membership item {item.Id} for customer {customerId}.");
            }
        }
    }
}
