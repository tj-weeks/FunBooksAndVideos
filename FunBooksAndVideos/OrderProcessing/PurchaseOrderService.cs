using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class PurchaseOrderService
    {
        public void ProcessOrder(PurchaseOrder order)
        {
            foreach (var item in order.PurchaseItems)
            {
                var orderProcessor = ItemProcessorFactory.GetPurchaseItemProcessor(item);
                orderProcessor.Process(item, order.CustomerId);
            }
        }
    }
}
