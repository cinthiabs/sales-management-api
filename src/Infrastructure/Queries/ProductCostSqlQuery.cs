namespace Infrastructure.Queries
{
    internal static class ProductCostSqlQuery
    {
        internal const string QueryCreateProductTotalCost = @"
        INSERT INTO ProductTotalCost (TotalProductCost, IdProduct, Active)
        OUTPUT inserted.Id        
        VALUES (@TotalProductCost,@Product, 1);";
        
        internal const string QuerySelectAllProductsCost = @"
            SELECT pt.Id AS IdProductTotalCost,
            pt.TotalProductCost,
            pt.IdProduct,
            pt.Active,
            pt.DateCreate,
            pt.DateEdit,
            pc.Id AS ProductCostId,
            pc.IdCost,
            pc.TotalProductPrice,
            pc.TotalQuantity,
            pc.QuantityRequired,
            pc.IngredientCost
        FROM 
            ProductTotalCost pt
        LEFT JOIN 
            ProductCost pc ON pt.Id = pc.id
        WHERE 
        1=1";

        internal const string QueryCreateProductCost = @"
        INSERT INTO ProductCost (IdProductTotalCost, IdCost, TotalProductPrice, TotalQuantity, QuantityRequired, IngredientCost)
        VALUES (@IdProductTotalCost, @IdCost, @TotalProductPrice, @TotalQuantity, @QuantityRequired, @IngredientCost);";

        internal const string QuerySelectProductCostID = @" 
            SELECT 
            pt.Id AS IdProductTotalCost,
            pt.IdProduct,
            pt.TotalProductCost,
            pt.Active,
            pt.DateCreate,
            pt.DateEdit,
            pc.Id AS ProductCostId,
            pc.IdCost,
            pc.TotalProductPrice,
            pc.TotalQuantity,
            pc.QuantityRequired,
            pc.IngredientCost
        FROM 
            ProductTotalCost pt
        LEFT JOIN 
            ProductCost pc ON pt.Id = pc.IdProductTotalCost
        WHERE 
        1=1
        AND pt.Id = @IdProductTotalCost;";
    }
}
