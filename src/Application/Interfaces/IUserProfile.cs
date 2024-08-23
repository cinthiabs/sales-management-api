using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserProfile
    {
        Task<Response<UserProfile>> GetAllUserProfileAsync();
        Task<Response<UserProfile>> GetByIdUserProfileAsync(int id);
        Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile);
    }
}
