using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ProductItemProcessor : IPurchaseItemProcessor
    {
        public void Process(IPurchaseItem item, int customerId)
        {
            // Logic to process product item
        }
    }
}
