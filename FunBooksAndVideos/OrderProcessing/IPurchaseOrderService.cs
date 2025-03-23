namespace FunBooksAndVideos.OrderProcessing
{
    public interface IPurchaseOrderService
    {
        public Task ProcessOrder(Classes.PurchaseOrder order);
    }
}
