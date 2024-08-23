using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public Task<Response<UserProfile>> GetByIdUserProfileAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserProfile>> GetUserProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
