using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.OrderProcessing
{
    public class ShippingSlipGenerator
    {
        public Task<bool> Generate(ProductItem item, int customerId)
        {
            // Logic to generate shipping slip
            return Task.FromResult(true);
        }
    }
}
