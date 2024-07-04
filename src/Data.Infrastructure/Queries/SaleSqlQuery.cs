namespace Data.Infrastructure.Queries
{
    internal static class SaleSqlQuery
    {
        internal const string QueryCreateSale = @"
        INSERT INTO Sale (DateSale, Name, Details, Quantity, Price, DateCreate)
        VALUES (@DateSale, @Name, @Details, @Quantity, @Price, @DateCreate);";

        internal const string QueryUpdateSale = @"
        UPDATE Sale
        SET
            DateSale = @DateSale,
            Name = @Name,
            Details = @Details,
            Quantity = @Quantity,
            Price = @Price,
            PAY = @Pay,
            DateEdit = @DateEdit
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

        internal const string GetRelQuantity = @"
        SELECT [Name], 
        sum(Quantity) AS Quantity,
        sum(price) as Price
        FROM Sale with(nolock)
        where DateSale  between @dateIni and @dateEnd
        GROUP BY [Name]
        ORDER BY Quantity DESC;";

    }
}
