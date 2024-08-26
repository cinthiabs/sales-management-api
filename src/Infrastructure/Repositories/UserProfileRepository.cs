using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository(IConfiguration configuration) : RepositoryBase(configuration), IUserProfileRepository
    {
        public async Task<Response<UserProfile>> GetByIdUserProfileAsync(int id)
        {
            var parameters = new { id };
            var profileId = await Connection.QueryFirstOrDefaultAsync<UserProfile>(UserProfileSqlQuery.QueryGetUserProfileId, parameters);
            if (profileId is null)
                return Response<UserProfile>.Failure(Status.noDatafound);

            return Response<UserProfile>.Success(profileId);
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
            var parameters = new
            {
                profile.Id,
                profile.FirstName,
                profile.LastName,
                profile.Phone,
                profile.Address,
                profile.City,
                profile.State,
                profile.ZipCode,
                @DateEdit = DateTime.Now
            };

            var update = await Connection.ExecuteAsync(UserProfileSqlQuery.QueryUpdateUserProfile, parameters);
            if (update is 0)
                return Response<UserProfile>.Failure(Status.UpdateFailure);

            var updated = await Connection.QueryFirstOrDefaultAsync<UserProfile>(UserProfileSqlQuery.QueryGetUserProfileId, new { profile.Id });
            return Response<UserProfile>.Success(updated!, Status.UpdatedSuccess);
        }
    }
}
