namespace Infrastructure.Queries
{
    internal static class UserCredentialsSqlQuery
    {
        internal const string QuerySelectUser = @"
        SELECT * FROM UserCredentials 
        WHERE Username = @Username
        AND Active = 1";

        internal const string QueryCreateUserCredentials = @"
        INSERT INTO UserCredentials (Username, PasswordHash, PasswordSalt, Email, Name)
        VALUES (@Username, @PasswordHash, @PasswordSalt, @Email, @Name);";

        internal const string QueryInactiveUserCredentials = @"
        UPDATE UserCredentials
        SET Active = 0,
            DateEdit = @DateEdit
        WHERE Username = @Username
        AND Id = @Id";
    }
}
