namespace Infrastructure.Queries
{
     internal static class ClientSqlQuery
    {
        internal const string QueryCreateClient = @"
        INSERT INTO Client (Name, Phone, Location, Active, DateCreate)
        OUTPUT inserted.Id
        VALUES (@Name, @Phone, @Location, @Active, @DateCreate);";

        internal const string QueryUpdateClient = @"
        UPDATE Client
        SET
            [Name] = @Name,
            Phone = @Phone,
            [Location] = @Location,
            Active = @Active,
            DateEdit = @DateEdit
        WHERE
            Id = @Id;";

        internal const string QuerySelectClients = @"
        SELECT * FROM Client";

        internal const string QueryDeleteClient = @"
        DELETE FROM Client
        WHERE Id = @Id";

        internal const string QueryGetByIdClient = @"
        SELECT * FROM Client
        WHERE Id = @Id";
    }
}