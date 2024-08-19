using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<bool>> CreateUserAsync(UserCredentials user);
        Task<Response<bool>> UpdateUserAsync(UserCredentials user, int Id);
        Task<Response<bool>> DeleteUserAsync(int Id);
        Task<Response<UserCredentials>> GetUserAsync(string Username, string Email);
    }
}
