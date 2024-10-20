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

        internal const string QueryUpdateUserProfile = @"
        UPDATE pro
        SET
            pro.FirstName = @FirstName,
            pro.LastName = @LastName,
            pro.Phone = @Phone,
            pro.Address = @Address,
            pro.City = @City,
            pro.State = @State,
            pro.ZipCode = @ZipCode,
            pro.DateEdit = @DateEdit
        FROM UserProfile pro
        INNER JOIN UserCredentials cre ON pro.UserId = cre.Id
        WHERE cre.Username = @Username";

    }
}
