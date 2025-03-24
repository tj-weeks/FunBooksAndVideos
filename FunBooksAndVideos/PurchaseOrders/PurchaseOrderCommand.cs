using FunBooksAndVideos.PurchaseOrder;
using MediatR;

namespace FunBooksAndVideos.PurchaseOrders
{
    public class PurchaseOrderCommand : IRequest<PurchaseOrderResponse>
    {
        public PurchaseOrderCommand(OrderItems.PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public OrderItems.PurchaseOrder PurchaseOrder { get; set; }
    }
}
