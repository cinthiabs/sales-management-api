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
        internal const string QuerySelectRelClients = @"
        SELECT s.Name [ProductName], c.Name [ClientName], s.Quantity, s.Price, Pay, s.DateSale
        FROM Sale S
        INNER JOIN Client C on s.IdClient = c.Id
        WHERE
        1 = 1 ";

        internal const string QuerySelectRelClientsById = @"
        AND C.id =  @Id";

        internal const string QuerySelectRelClientsByDate = @"
        AND s.DateSale between @dateIni and @dateEnd";

        internal const string QuerySelectClients = @"
        SELECT * FROM Client";

        internal const string QueryDeleteClient = @"
        DELETE FROM Client
        WHERE Id = @Id";

        internal const string QueryGetByIdClient = @"
        SELECT * FROM Client
        WHERE Id = @Id";

        internal const string QueryGetClientByName = @"
        SELECT * FROM Client
        WHERE Name LIKE '%' + @Name + '%'";
     }
}