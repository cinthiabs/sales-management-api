namespace Infrastructure.Queries
{
    internal static class CostSqlQuery
    {
        internal const string QueryCreateCost = @"
        INSERT INTO Costs (Quantity, Name, DateCost, UnitPrice, TotalPrice, DateCreate)
        OUTPUT inserted.Id
        VALUES (@Quantity, @Name, @DateCost, @UnitPrice, @TotalPrice, @DateCreate);";

        internal const string QueryUpdateCost = @"
        UPDATE Costs
        SET
            Quantity = @Quantity,
            Name = @Name,
            DateCost = @DateCost,
            UnitPrice = @UnitPrice,
            TotalPrice = @TotalPrice,
            DateEdit = @DateEdit
        WHERE
            Id = @Id;";

        internal const string QuerySelectCost = @"
        SELECT * FROM Costs";

        internal const string QueryDeleteCost = @"
        DELETE FROM Costs
        WHERE Id = @Id";

        internal const string QueryGetByIdCost = @"
        SELECT * FROM Costs
        WHERE Id = @Id";

        internal const string QueryByCostParameters = @"
        SELECT * FROM Costs
        WHERE 
        DateCost = @DateCost 
        AND UnitPrice = @UnitPrice  
        AND Quantity = @Quantity
        AND Name = @Name";

        internal const string GetRelCostPrice = @"
        SELECT [Name], 
        SUM(totalPrice) as TotalPrice
        FROM Costs with(nolock)
        where DateCost between @dateIni and @dateEnd
        GROUP BY [Name];";
    }
}
