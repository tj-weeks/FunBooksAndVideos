using FunBooksAndVideos.OrderItems;

namespace FunBooksAndVideos.OrderProcessing
{
    public interface IPurchaseItemProcessor
    {
        public Task Process(IPurchaseItem item, int customerId);
    }
}
