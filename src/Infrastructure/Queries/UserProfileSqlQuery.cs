namespace Infrastructure.Queries
{
    internal static class UserProfileSqlQuery
    {
        internal const string QueryCreateUserProfile = @"
        INSERT INTO UserProfile (UserId,FirstName,AccessLevelId)
        VALUES (@UserId, @FirstName, @AccessLevelId);";
    }
}
