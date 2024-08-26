using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class UserProfileService(IUserProfileRepository userProfileRepository) : IUserProfile
    {
        private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;

        public async Task<Response<UserProfile>> GetByIdUserProfileAsync(int id)
        {
            return await _userProfileRepository.GetByIdUserProfileAsync(id);
        }

        public async Task<Response<UserProfile>> GetAllUserProfileAsync()
        {
            return await _userProfileRepository.GetUserProfileAsync();
        }

        public async Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile)
        {
            var existUser = await _userProfileRepository.GetByIdUserProfileAsync(profile.Id);
            if (existUser.IsSuccess)
            {
                var updated = await _userProfileRepository.UpdateUserProfileAsync(profile);
                return updated;
            }
            return existUser;
        }
    }
}
