using FunBooksAndVideos.Classes;

namespace FunBooksAndVideos.Events
{
    public class ActivateMembershipEvent
    {
        public required MembershipItem Item { get; set; }
        public required int CustomerId { get; set; }
    }
}
