namespace Infrastructure.Queries
{
    internal static class UserCredentialsSqlQuery
    {
        internal const string QuerySelectUser = @"
        SELECT * FROM UserCredentials 
        WHERE Email = @Email
        AND Active = 1";

        internal const string QueryCreateUserCredentials = @"
        INSERT INTO UserCredentials (Username, Name, PasswordHash, PasswordSalt, Email)
        VALUES (@Username, @Name, @PasswordHash, @PasswordSalt, @Email)
        SELECT SCOPE_IDENTITY()";

        internal const string QueryInactiveUserCredentials = @"
        UPDATE UserCredentials
        SET Active = 0,
            DateEdit = @DateEdit
        WHERE Username = @Username
        AND Id = @Id";

        internal const string QueryUpdateUserCredentials = @"
        UPDATE UserCredentials
        SET Token = @Token,
            TokenExpiration = @TokenExpiration,
            LastLogin = @LastLogin,
            DateEdit = @DateEdit
        WHERE 
        Id = @Id
        AND Active = 1";


    }
}
