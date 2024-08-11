using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository(IConfiguration configuration) : RepositoryBase(configuration), IUserRepository
    {
        public async Task<Response<bool>> CreateUserAsync(UserCredentials user)
        {
            using var transaction = Connection.BeginTransaction();
            try
            {
                var parameters = new
                {
                    user.Username,
                    user.Name,
                    user.Email,
                    user.PasswordHash,
                    user.PasswordSalt
                };
                var userId = await Connection.QueryFirstOrDefaultAsync<int>(UserCredentialsSqlQuery.QueryCreateUserCredentials, parameters, transaction);
                if (userId > 0)
                    await CreateUserProfileAsync(userId,user.Name,transaction);

                transaction.Commit();
                return Response<bool>.Success(true, Status.InsertSuccess);

            }
            catch (Exception)
            {
                transaction.Rollback();
                return Response<bool>.Failure(Status.InsertFailure);
            }

        }
        private async Task<Response<bool>> CreateUserProfileAsync(int UserId, string Name, IDbTransaction transaction, int? AccessLevelId = 0)
        {
            if (AccessLevelId == 0)
                AccessLevelId = (int)AccessLevel.User;

            var parameters = new
            {
                AccessLevelId,
                @FirstName = Name,
                UserId
            };
            var insert = await Connection.ExecuteScalarAsync(UserProfileSqlQuery.QueryCreateUserProfile, parameters, transaction);
            if (insert is 0)
                return Response<bool>.Failure(Status.InsertFailure);

            return Response<bool>.Success(true, Status.InsertSuccess);
        }

        public async Task<Response<bool>> DeleteUserAsync(int Id)
        {
            var parameters = new { Id };
            var delete = await Connection.ExecuteAsync(UserCredentialsSqlQuery.QueryInactiveUserCredentials, parameters);
            if (delete is 0)
                return Response<bool>.Failure(Status.DeleteFailure);

            return Response<bool>.Success(true, Status.DeletedSuccess);
        }

        public async Task<Response<UserCredentials>> GetUserAsync(string Username, string Email)
        {
            var parameters = new { Username, Email };
            var user = await Connection.QueryFirstOrDefaultAsync<UserCredentials>(UserCredentialsSqlQuery.QuerySelectUser, parameters);
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
