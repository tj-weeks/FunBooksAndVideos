using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public interface IPurchaseItemProcessor
    {
        public void Process(IPurchaseItem item, int customerId);
    }
}
