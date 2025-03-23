﻿using FunBooksAndVideos.Entities;

namespace FunBooksAndVideos.Events
{
    public class ActivateMembershipEventHandler : IHandleMessages<ActivateMembershipEvent>
    {
        private readonly ICustomer _customer;
        private readonly ILogger<ActivateMembershipEventHandler> _logger;

        public ActivateMembershipEventHandler(
            ICustomer customer,
            ILogger<ActivateMembershipEventHandler> logger)
        {
            _customer = customer;
            _logger = logger;
        }

        public async Task Handle(ActivateMembershipEvent message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Handling ActivateMembershipEvent for customer {message.CustomerId}");
            await _customer.UpdateMembership(message.Item.MembershipType, message.CustomerId);
        }
    }
}
