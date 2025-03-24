using FunBooksAndVideos.Enums;

namespace FunBooksAndVideos.Services
{
    public class CustomerService : ICustomerService
    {
        public async Task<bool> UpdateMembership(Membership.Type membershipType, int customerId)
        {
            // Logic to update membership on customer account
            return await Task.FromResult(true);
        }
    }
}
