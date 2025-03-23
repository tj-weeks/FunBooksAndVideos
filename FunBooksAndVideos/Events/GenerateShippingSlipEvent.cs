using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.Events
{
    public class GenerateShippingSlipEvent : IEvent
    {
        public required ProductItem Item { get; set; }
        public required int CustomerId { get; set; }
    }
}
