using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ProductCostRepository(IConfiguration configuration) : RepositoryBase(configuration), IProductCostRepository
    {
        public async Task<Response<ProductTotalCosts>> CreateProductTotalCostAsync(ProductTotalCosts productCost, int product)
        {
            using var transaction = Connection.BeginTransaction();
            try
            {
                var idProductTotalCost = await Connection.ExecuteScalarAsync<int>(ProductCostSqlQuery.QueryCreateProductTotalCost, new { productCost.TotalProductCost },transaction);
                if (idProductTotalCost > 0)
                {
                    var result = await CreateProductCostAsync(productCost.ProductCost, product, idProductTotalCost, transaction);
                    if (result.IsFailure)
                    {
                        throw new Exception(result.Message);
                    }
                    else
                    {
                        transaction.Commit();
                        var getProductCost = await GetProductCostByIdAsync(idProductTotalCost);
                        return Response<ProductTotalCosts>.Success(getProductCost.Data!, Status.InsertSuccess);
                    }
                }

                return Response<ProductTotalCosts>.Failure(Status.InsertFailure);
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Response<ProductTotalCosts>.Failure(Status.InsertFailure);
            }
        }

        private async Task<Response<ProductCost>> CreateProductCostAsync(List<ProductCost> productCosts,int idProduct, int idProductTotalCost, IDbTransaction transaction)
        {
            var result = 0;
            try
            {
                foreach (var cost in productCosts)
                {
                    cost.IdProduct = idProduct;
                    var parameters = new
                    {
                        idProductTotalCost,
                        cost.IdProduct,
                        cost.IdCost,
                        cost.TotalProductPrice,
                        cost.TotalQuantity,
                        cost.QuantityRequired,
                        cost.IngredientCost,
                        DateCreate = DateTime.Now
                    };
                    result = await Connection.ExecuteAsync(ProductCostSqlQuery.QueryCreateProductCost, parameters, transaction);
                }
                if (result is not 0)
                    return Response<ProductCost>.Success(productCosts.FirstOrDefault()!);

                return Response<ProductCost>.Failure(Status.InsertFailure);
            }
            catch (Exception)
            {
                return Response<ProductCost>.Failure(Status.InsertFailure);
            }

        }

        public async Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id)
        {
            var productTotalCostDictionary = new Dictionary<int, ProductTotalCosts>();

            var productTotalCosts = await Connection.QueryAsync<ProductTotalCosts, ProductCost, ProductTotalCosts>(
                ProductCostSqlQuery.QuerySelectProductCostID,
                (pt, pc) =>
                {
                    if (!productTotalCostDictionary.TryGetValue(pt.IdProductTotalCost, out var productTotalCostEntry))
                    {
                        productTotalCostEntry = pt;
                        productTotalCostEntry.ProductCost = new List<ProductCost>();
                        productTotalCostDictionary.Add(pt.IdProductTotalCost, productTotalCostEntry);
                    }
                    if (pc != null)
                    {
                        pc.IdProductTotalCost = pt.IdProductTotalCost;
                        productTotalCostEntry.ProductCost.Add(pc);
                    }
                    return productTotalCostEntry;
                },
                new { IdProductTotalCost = id },
                splitOn: "ProductCostId"
            );

            var productTotalCost = productTotalCostDictionary.Values.FirstOrDefault();

            if (productTotalCost == null)
                return Response<ProductTotalCosts>.Failure(Status.noDatafound);

            return Response<ProductTotalCosts>.Success(productTotalCost);
        }

        public Task<Response<ProductTotalCosts>> UpdateProductCostAsync(ProductTotalCosts productCost, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProductTotalCosts>> GetAllProductCostAsync()
        {
            var productTotalCost = await Connection.QueryAsync<ProductTotalCosts>(ProductCostSqlQuery.QuerySelectAllProductsCost);
            if (!productTotalCost.Any())
                return Response<ProductTotalCosts>.Failure(Status.noDatafound);

            return Response<ProductTotalCosts>.Success(productTotalCost.ToArray());
        }
    }
}
