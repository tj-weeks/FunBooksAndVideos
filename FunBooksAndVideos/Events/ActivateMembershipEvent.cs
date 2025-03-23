using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.Events
{
    public class ActivateMembershipEvent : IEvent
    {
        public required MembershipItem Item { get; set; }
        public required int CustomerId { get; set; }
    }
}
