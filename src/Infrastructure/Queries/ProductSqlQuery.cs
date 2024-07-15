namespace Infrastructure.Queries
{
    internal static class ProductSqlQuery
    {
        internal const string QueryCreateProduct = @"
        INSERT INTO Product (Name, Details, Active, Price, DateCreate)
        VALUES (@Name, @Details, @Active, @Price, @DateCreate);";

        internal const string QueryUpdateProduct = @"
        UPDATE Product
        SET
            Name = @Name,
            Details = @Details,
            Active = @Active,
            Price = @Price,
            DateEdit = @DateEdit
        WHERE
            Id = @Id;";
        
        internal const string QuerySelectProduct = @"
        SELECT * FROM Product";
        
        internal const string QueryDeleteProduct = @"
        DELETE FROM Product
        WHERE Id = @Id";
        
        internal const string QueryGetByIdProduct = @"
        SELECT * FROM Product
        WHERE Id = @Id";
    }
}
