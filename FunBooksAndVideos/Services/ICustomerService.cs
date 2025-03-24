using FunBooksAndVideos.Enums;

namespace FunBooksAndVideos.Services
{
    public interface ICustomerService
    {
        public Task<bool> UpdateMembership(Membership.Type membershipType, int customerId);
    }
}
