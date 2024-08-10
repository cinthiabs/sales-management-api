using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUser
    {
        Task<Response<bool>> CreateUserAsync(Login login);
        Task<Response<bool>> UpdateUserAsync(Login login);
        Task<Response<bool>> DeleteUserAsync(Login login);
        void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Task<Response<UserCredentials>> GetUserAsync(string username, string password);
    }
}
