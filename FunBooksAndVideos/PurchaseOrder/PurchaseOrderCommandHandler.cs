using FunBooksAndVideos.Services;
using MediatR;

namespace FunBooksAndVideos.PurchaseOrder
{
    public class PurchaseOrderCommandHandler : IRequestHandler<PurchaseOrderCommand, PurchaseOrderResponse>
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ILogger<PurchaseOrderCommandHandler> _logger;

        public PurchaseOrderCommandHandler(
            IPurchaseOrderService purchaseOrderService,
            ILogger<PurchaseOrderCommandHandler> logger) 
        {
            _purchaseOrderService = purchaseOrderService;
            _logger = logger;
        }

        public async Task<PurchaseOrderResponse> Handle(PurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling PurchaseOrder request {request.PurchaseOrder.Id} for customer {request.PurchaseOrder.CustomerId}");
            await _purchaseOrderService.ProcessOrder(request.PurchaseOrder);
            return new PurchaseOrderResponse();
        }
    }
}
