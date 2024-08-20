using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUser
    {
        Task<Response<bool>> CreateUserAsync(Login login);
        Task<Response<bool>> UpdateUserAsync(Login login);
        Task<Response<bool>> DeleteUserAsync(Login login);
        void CreatePassword(string password, out string passwordHash, out string passwordSalt);
        Task<Response<UserCredentials>> GetUserAsync(Login login);
    }
}
