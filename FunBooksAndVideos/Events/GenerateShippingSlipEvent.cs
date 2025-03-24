using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.Events
{
    public class GenerateShippingSlipEvent
    {
        public required ProductItem Item { get; set; }
        public required int CustomerId { get; set; }
    }
}
