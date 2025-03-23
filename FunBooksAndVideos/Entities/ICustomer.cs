using FunBooksAndVideos.Enums;

namespace FunBooksAndVideos.Entities
{
    public interface ICustomer
    {
        public Task<bool> UpdateMembership(Membership.Type membershipType, int customerId);
    }
}
