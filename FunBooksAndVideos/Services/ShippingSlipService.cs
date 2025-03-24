using FunBooksAndVideos.OrderItems;

namespace FunBooksAndVideos.Services
{
    public class ShippingSlipService : IShippingSlipService
    {
        public Task<bool> Generate(ProductItem item, int customerId)
        {
            // Logic to generate shipping slip
            return Task.FromResult(true);
        }
    }
}
