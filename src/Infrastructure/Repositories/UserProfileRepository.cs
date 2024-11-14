using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository(IConfiguration configuration, ILogger<UserProfileRepository> logger) : RepositoryBase(configuration), IUserProfileRepository
    {
        private readonly ILogger<UserProfileRepository> _logger = logger;
        public async Task<Response<UserProfile>> GetByUsernameProfileAsync(string username)
        {
            var parameters = new { username };
            var profile = await Connection.QueryFirstOrDefaultAsync<UserProfile>(UserProfileSqlQuery.QueryGetUserProfile, parameters);
            if (profile is null)
                return Response<UserProfile>.Failure(Status.noDatafound);

            return Response<UserProfile>.Success(profile);
        }

        public async Task<Response<UserProfile>> GetUserProfileAsync()
        {
            var profile = await Connection.QueryAsync<UserProfile>(UserProfileSqlQuery.QueryGetAllUserProfile);
            if (!profile.Any())
                return Response<UserProfile>.Failure(Status.noDatafound);

            return Response<UserProfile>.Success(profile.ToArray());
        }

        public async Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfile profile)
        {
            var sqlBuilder = new StringBuilder(@"
            UPDATE pro SET ");

            var parameters = new DynamicParameters();
            parameters.Add("Username", profile.Username);

            sqlBuilder.Append("pro.DateEdit = @DateEdit, ");
            parameters.Add("DateEdit", DateTime.Now);

            sqlBuilder.Append("pro.AccessLevelId = @AccessLevelId, ");
            parameters.Add("AccessLevelId", profile.AccessLevelId);

            if (!string.IsNullOrEmpty(profile.Image))
            {
                sqlBuilder.Append("pro.Image = @Image, ");
                parameters.Add("Image", profile.Image);
            }

            if (!string.IsNullOrEmpty(profile.Number))
            {
                sqlBuilder.Append("pro.Number = @Number, ");
                parameters.Add("Number", profile.Number);
            }

            if (!string.IsNullOrEmpty(profile.Neighborhood))
            {
                sqlBuilder.Append("pro.Neighborhood = @Neighborhood, ");
                parameters.Add("Neighborhood", profile.Neighborhood);
            }

            if (!string.IsNullOrEmpty(profile.FirstName))
            {
                sqlBuilder.Append("pro.FirstName = @FirstName, ");
                parameters.Add("FirstName", profile.FirstName);
            }
            if (!string.IsNullOrEmpty(profile.LastName))
            {
                sqlBuilder.Append("pro.LastName = @LastName, ");
                parameters.Add("LastName", profile.LastName);
            }
            if (!string.IsNullOrEmpty(profile.Phone))
            {
                sqlBuilder.Append("pro.Phone = @Phone, ");
                parameters.Add("Phone", profile.Phone);
            }
            if (!string.IsNullOrEmpty(profile.Address))
            {
                sqlBuilder.Append("pro.Address = @Address, ");
                parameters.Add("Address", profile.Address);
            }
            if (!string.IsNullOrEmpty(profile.City))
            {
                sqlBuilder.Append("pro.City = @City, ");
                parameters.Add("City", profile.City);
            }
            if (!string.IsNullOrEmpty(profile.State))
            {
                sqlBuilder.Append("pro.State = @State, ");
                parameters.Add("State", profile.State);
            }
            if (!string.IsNullOrEmpty(profile.ZipCode))
            {
                sqlBuilder.Append("pro.ZipCode = @ZipCode, ");
                parameters.Add("ZipCode", profile.ZipCode);
            }

            sqlBuilder.Length -= 2;
            sqlBuilder.Append(@"
            FROM UserProfile pro
            INNER JOIN UserCredentials cre ON pro.UserId = cre.Id
            WHERE cre.Username = @Username");

            var update = await Connection.ExecuteAsync(sqlBuilder.ToString(), parameters);
            if (update is 0)
                return Response<UserProfile>.Failure(Status.UpdateFailure);

            var updated = await Connection.QueryFirstOrDefaultAsync<UserProfile>(
                UserProfileSqlQuery.QueryGetUserProfile, new { profile.Username });

            return Response<UserProfile>.Success(updated!, Status.UpdatedSuccess);
        }

    }
}
