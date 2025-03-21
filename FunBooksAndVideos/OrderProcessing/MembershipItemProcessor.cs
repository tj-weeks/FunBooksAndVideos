using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class MembershipItemProcessor : IPurchaseItemProcessor
    {
        public void Process(IPurchaseItem item, int customerId)
        {
            // Logic to process membership item
        }
    }
}
