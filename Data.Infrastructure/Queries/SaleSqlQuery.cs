namespace Data.Infrastructure.Queries
{
    internal static class SaleSqlQuery
    {
        internal const string QueryCreateSaleList = @"
        INSERT INTO Sale (DateSale, Name, Details, Quantity, Price, DataCreate)
        VALUES (@DateSale, @Name, @Details, @Quantity, @Price, @DataCreate);";
        
        internal const string QueryCreateSale = @"";
        internal const string QueryUpdateSale = @"";
        internal const string QuerySelectSale = @"";
        internal const string QueryDeleteSale = @"";
        internal const string QueryGetByIdSale = @"";
    }
}
