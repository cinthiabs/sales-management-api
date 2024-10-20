using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<Response<UserProfile>> GetUserProfileAsync();
        Task<Response<UserProfile>> GetByUsernameProfileAsync(string username);
        Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile);
    }
}
