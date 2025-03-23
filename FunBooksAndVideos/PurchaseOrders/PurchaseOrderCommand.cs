using FunBooksAndVideos.PurchaseOrder;
using MediatR;

namespace FunBooksAndVideos.PurchaseOrders
{
    public class PurchaseOrderCommand : IRequest<PurchaseOrderResponse>
    {
        public PurchaseOrderCommand(Classes.PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public Classes.PurchaseOrder PurchaseOrder { get; set; }
    }
}
