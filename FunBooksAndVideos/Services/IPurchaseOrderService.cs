namespace FunBooksAndVideos.Services
{
    public interface IPurchaseOrderService
    {
        public Task ProcessOrder(OrderItems.PurchaseOrder order);
    }
}
