using FunBooksAndVideos.Enums;

namespace FunBooksAndVideos.Entities
{
    public class Customer : ICustomer
    {
        public async Task<bool> UpdateMembership(Membership.Type membershipType, int customerId)
        {
            // Logic to update membership on customer account
            return await Task.FromResult(true);
        }
    }
}
