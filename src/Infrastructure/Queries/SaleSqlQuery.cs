namespace Infrastructure.Queries
{
    internal static class SaleSqlQuery
    {
        internal const string QueryCreateSale = @"
        INSERT INTO Sale (IdProduct,IdClient, DateSale, Name, Details, Quantity, Price, Pay, DateCreate)
        OUTPUT inserted.Id
        VALUES (@IdProduct, @IdClient, @DateSale, @Name, @Details, @Quantity, @Price, @Pay, @DateCreate);";

        internal const string QueryUpdateSale = @"
        UPDATE Sale
        SET
            DateSale = @DateSale,
            Name = @Name,
            Details = @Details,
            Quantity = @Quantity,
            Price = @Price,
            Pay = @Pay,
            IdProduct = @IdProduct,
            IdClient = @IdClient,
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
        AND Price = @Price  
        AND Quantity = @Quantity
        AND Name = @Name
        AND (@IdProduct IS NULL OR IdProduct = @IdProduct)";


        internal const string GetRelQuantity = @"
            SELECT [Name], 
            SUM(Quantity) AS Quantity,
            SUM(price) as Price,
            SUM(CASE WHEN Pay = 1 THEN Quantity ELSE 0 END) AS Paid,
            SUM(CASE WHEN Pay = 0 THEN Quantity ELSE 0 END) AS NotPaid
            FROM Sale with(nolock)
            where DateSale  between @dateIni and @dateEnd
            GROUP BY [Name]
            ORDER BY Quantity DESC;";

    }
}
