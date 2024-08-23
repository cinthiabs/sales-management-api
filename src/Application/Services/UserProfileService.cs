using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class UserProfileService : IUserProfile
    {

        public Task<Response<UserProfile>> GetByIdUserProfileAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserProfile>> GetAllUserProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
