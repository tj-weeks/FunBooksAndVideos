using FunBooksAndVideos.Services;

namespace FunBooksAndVideos.Events
{
    public class ActivateMembershipEventHandler
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<ActivateMembershipEventHandler> _logger;

        public ActivateMembershipEventHandler(
            ICustomerService customerService,
            ILogger<ActivateMembershipEventHandler> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        public async Task Handle(ActivateMembershipEvent message)
        {
            _logger.LogInformation($"Handling ActivateMembershipEvent for customer {message.CustomerId}");
            await _customerService.UpdateMembership(message.Item.MembershipType, message.CustomerId);
        }
    }
}
