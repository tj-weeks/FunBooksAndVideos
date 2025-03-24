using FunBooksAndVideos.Enums;

namespace FunBooksAndVideos.OrderItems
{
    public class MembershipItem : IPurchaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Membership.Type MembershipType { get; set; }
    }
}
