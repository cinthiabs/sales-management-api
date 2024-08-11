namespace Infrastructure.Queries
{
    internal static class UserCredentialsSqlQuery
    {
        internal const string QuerySelectUser = @"
        SELECT * FROM UserCredentials 
        WHERE Username = @Username
        AND Email = @Email
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

    }
}
