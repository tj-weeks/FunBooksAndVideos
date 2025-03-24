using FunBooksAndVideos.Services;

namespace FunBooksAndVideos.Events
{
    public class ActivateMembershipEventHandler
    {
        private readonly ICustomerService _customer;
        private readonly ILogger<ActivateMembershipEventHandler> _logger;

        public ActivateMembershipEventHandler(
            ICustomerService customer,
            ILogger<ActivateMembershipEventHandler> logger)
        {
            _customer = customer;
            _logger = logger;
        }

        public async Task Handle(ActivateMembershipEvent message)
        {
            _logger.LogInformation($"Handling ActivateMembershipEvent for customer {message.CustomerId}");
            await _customer.UpdateMembership(message.Item.MembershipType, message.CustomerId);
        }
    }
}
