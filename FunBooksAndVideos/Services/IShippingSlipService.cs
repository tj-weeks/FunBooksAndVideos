using FunBooksAndVideos.OrderItems;

namespace FunBooksAndVideos.Services
{
    public interface IShippingSlipService
    {
        public Task<bool> Generate(ProductItem item, int customerId);
    }
}
