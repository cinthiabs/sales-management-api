using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class UserRepository(IConfiguration configuration) : RepositoryBase(configuration), IUserRepository
    {
        public async Task<Response<bool>> CreateUserAsync(UserCredentials user)
        {
            var parameters = new
            {
                user.Username,
                user.Name,
                user.Email,
                user.PasswordHash,
                user.PasswordSalt
            };
            var insert = await _conn.ExecuteScalarAsync(UserCredentialsSqlQuery.QueryCreateUserCredentials, parameters);
            if(insert is 0)
                return Response<bool>.Failure(Status.InsertFailure);
           
            return Response<bool>.Success(true, Status.InsertSuccess);
        }

        public async Task<Response<bool>> DeleteUserAsync(int Id)
        {
            var parameters = new { Id };
            var delete = await _conn.ExecuteAsync(UserCredentialsSqlQuery.QueryInactiveUserCredentials, parameters);
            if (delete is 0)
                return Response<bool>.Failure(Status.DeleteFailure);

            return Response<bool>.Success(true, Status.DeletedSuccess);
        }

        public async Task<Response<UserCredentials>> GetUserAsync(string Username)
        {
            var parameters = new { Username };
            var user = await _conn.QueryFirstOrDefaultAsync<UserCredentials>(UserCredentialsSqlQuery.QuerySelectUser, parameters);
            if (user is null)
                return Response<UserCredentials>.Failure(Status.noDatafound);

            return Response<UserCredentials>.Success(user);
        }

        public Task<Response<bool>> UpdateUserAsync(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
