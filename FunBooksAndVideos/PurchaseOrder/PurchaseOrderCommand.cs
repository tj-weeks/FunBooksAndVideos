using MediatR;

namespace FunBooksAndVideos.PurchaseOrder
{
    public class PurchaseOrderCommand : IRequest<PurchaseOrderResponse>
    {
        public PurchaseOrderCommand(OrderItems.PurchaseOrderItem purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public OrderItems.PurchaseOrderItem PurchaseOrder { get; set; }
    }
}
