namespace Infrastructure.Queries
{
    internal static class UserProfileSqlQuery
    {
        internal const string QueryCreateUserProfile = @"
        INSERT INTO UserProfile (UserId,FirstName,AccessLevelId)
        VALUES (@UserId, @FirstName, @AccessLevelId);";

        internal const string QueryGetUserProfileId = @"
        SELECT * FROM UserProfile WHERE Id=@Id";

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
