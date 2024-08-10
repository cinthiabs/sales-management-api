using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<bool>> CreateUserAsync(UserCredentials user);
        Task<Response<bool>> UpdateUserAsync(Login login);
        Task<Response<bool>> DeleteUserAsync(int Id);
        Task<Response<UserCredentials>> GetUserAsync(string Username);
    }
}
