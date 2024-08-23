using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserProfile
    {
        Task<Response<UserProfile>> GetUserProfileAsync();
        Task<Response<UserProfile>> GetByIdUserProfileAsync(int id);
        Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile);
    }
}
