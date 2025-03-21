using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ItemProcessorFactory
    {
        public static IPurchaseItemProcessor GetPurchaseItemProcessor(IPurchaseItem item)
        {
            if (item is ProductItem)
            {
                return new ProductItemProcessor();
            }
            else
            {
                return new MembershipItemProcessor();
            }
        }
    }
}
