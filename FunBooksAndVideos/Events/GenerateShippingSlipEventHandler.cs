using FunBooksAndVideos.OrderProcessing;

namespace FunBooksAndVideos.Events
{
    public class GenerateShippingSlipEventHandler : IHandleMessages<GenerateShippingSlipEvent>
    {
        private readonly ShippingSlipGenerator _shippingSlipGenerator;
        private readonly ILogger<GenerateShippingSlipEventHandler> _logger;

        public GenerateShippingSlipEventHandler(
            ShippingSlipGenerator shippingSlipGenerator,
            ILogger<GenerateShippingSlipEventHandler> logger)
        {
            _shippingSlipGenerator = shippingSlipGenerator;
            _logger = logger;
        }

        public async Task Handle(GenerateShippingSlipEvent message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Handling GenerateShippingSlipEvent for customer {message.CustomerId}");
            await _shippingSlipGenerator.Generate(message.Item, message.CustomerId);
        }
    }
}
