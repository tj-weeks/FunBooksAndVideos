using FunBooksAndVideos.Services;

namespace FunBooksAndVideos.Events
{
    public class GenerateShippingSlipEventHandler
    {
        private readonly IShippingSlipService _shippingSlipService;
        private readonly ILogger<GenerateShippingSlipEventHandler> _logger;

        public GenerateShippingSlipEventHandler(
            IShippingSlipService shippingSlipService,
            ILogger<GenerateShippingSlipEventHandler> logger)
        {
            _shippingSlipService = shippingSlipService;
            _logger = logger;
        }

        public async Task Handle(GenerateShippingSlipEvent message)
        {
            _logger.LogInformation($"Handling GenerateShippingSlipEvent for customer {message.CustomerId}");
            await _shippingSlipService.Generate(message.Item, message.CustomerId);
        }
    }
}
