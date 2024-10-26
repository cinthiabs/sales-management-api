namespace Infrastructure.Queries
{
    internal static class UserProfileSqlQuery
    {
        internal const string QueryCreateUserProfile = @"
        INSERT INTO UserProfile (UserId,FirstName,AccessLevelId)
        VALUES (@UserId, @FirstName, @AccessLevelId);";

        internal const string QueryGetUserProfile = @"
        select pro.UserId,pro.Id,Image, Username, Email,  pro.AccessLevelId, pro.FirstName, pro.LastName, pro.Phone, pro.Address, pro.City, pro.State, 
        pro.ZipCode, pro.DateCreate, pro.DateEdit from UserProfile pro
        inner join UserCredentials cre on pro.UserId = cre.Id
        WHERE Username=@username";

        internal const string QueryGetAllUserProfile = @"
        SELECT * FROM UserProfile(nolock)";
    }
}
