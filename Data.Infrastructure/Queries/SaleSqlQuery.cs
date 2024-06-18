namespace Data.Infrastructure.Queries
{
    internal static class SaleSqlQuery
    {
        internal const string QueryCreateSale = @"
        INSERT INTO Sale (DateSale, Name, Details, Quantity, Price, DataCreate)
        VALUES (@DateSale, @Name, @Details, @Quantity, @Price, @DataCreate);";

        internal const string QueryUpdateSale = @"
        UPDATE Sale
        SET
            DateSale = @DateSale,
            Name = @Name,
            Details = @Details,
            Quantity = @Quantity,
            Price = @Price,
            PAY = @Pay,
            DataEdit = @DataEdit
        WHERE
            Id = @Id;";
        
        internal const string QuerySelectSale = @"
        SELECT * FROM Sale";
        
        internal const string QueryDeleteSale = @"
        DELETE FROM Sale
        WHERE Id = @Id";
        
        internal const string QueryGetByIdSale = @"
        SELECT * FROM Sale
        WHERE Id = @Id";

        internal const string QueryBySaleParameters = @"
        SELECT * FROM Sale
        WHERE 
        DateSale = @DateSale 
        and Price = @Price  
        and Quantity = @Quantity
        and Name = @Name";  
    }
}
