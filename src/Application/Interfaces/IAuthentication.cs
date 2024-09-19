using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthentication
    {
        Task<Response<UserCredentialsDto>> AuthenticationAsync(LoginDto loginDto);
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}
