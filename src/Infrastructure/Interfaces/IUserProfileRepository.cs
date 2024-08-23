using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<Response<UserProfile>> GetUserProfileAsync();
        Task<Response<UserProfile>> GetByIdUserProfileAsync(int id);
        Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile);
    }
}
