using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserProfile
    {
        Task<Response<UserProfile>> GetAllUserProfileAsync();
        Task<Response<UserProfile>> GetByUsernameProfileAsync(string username);
        Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfileDto profile, string username);
    }
}
