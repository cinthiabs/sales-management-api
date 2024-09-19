using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUser
    {
        Task<Response<bool>> CreateUserAsync(LoginDto loginDto);
        Task<Response<bool>> UpdateUserAsync(LoginDto loginDto);
        Task<Response<bool>> DeleteUserAsync(LoginDto loginDto);
        void CreatePassword(string password, out string passwordHash, out string passwordSalt);
        Task<Response<UserCredentials>> GetUserAsync(LoginDto loginDto);
    }
}
