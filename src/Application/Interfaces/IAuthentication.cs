using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthentication
    {
        Task<Response<UserCredentials>> AuthenticationAsync(Login login);
    }
}
