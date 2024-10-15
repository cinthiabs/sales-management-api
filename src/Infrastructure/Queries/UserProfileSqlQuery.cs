namespace Infrastructure.Queries
{
    internal static class UserProfileSqlQuery
    {
        internal const string QueryCreateUserProfile = @"
        INSERT INTO UserProfile (UserId,FirstName,AccessLevelId)
        VALUES (@UserId, @FirstName, @AccessLevelId);";

        internal const string QueryGetUserProfileId = @"
        select pro.UserId,pro.Id, Username, Email,  pro.AccessLevelId, pro.FirstName, pro.LastName, pro.Phone, pro.Address, pro.City, pro.State, 
        pro.ZipCode, pro.DateCreate, pro.DateEdit from UserProfile pro
        inner join UserCredentials cre on pro.UserId = cre.Id
        WHERE pro.Id=@Id";

        internal const string QueryGetAllUserProfile = @"
        SELECT * FROM UserProfile(nolock)";

        internal const string QueryUpdateUserProfile = @"
        UPDATE UserProfile 
        SET
           [FirstName] = @FirstName
          ,[LastName] = @LastName
          ,[Phone] = @Phone
          ,[Address] = @Address
          ,[City] = @City
          ,[State] = @State
          ,[ZipCode] = @ZipCode
          ,[DateEdit] = @DateEdit
        WHERE
        Id = @Id ";

    }
}
