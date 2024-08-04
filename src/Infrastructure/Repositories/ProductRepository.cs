using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class ProductRepository(IConfiguration configuration) : RepositoryBase(configuration), IProductRepository
    {
        public async Task<Response<Products>> CreateProductAsync(Products prod)
        {
            var parameters = new
            {
                prod.Name,
                prod.Details,
                prod.Active,
                prod.Price,
                DateCreate = DateTime.Now
            };

            var Id = await _conn.ExecuteScalarAsync(ProductSqlQuery.QueryCreateProduct, parameters);
            var created = await _conn.QueryFirstOrDefaultAsync<Products>(ProductSqlQuery.QueryGetByIdProduct, new { Id });
            if(created is null)
                return Response<Products>.Failure(Status.noDatafound);
            
            return Response<Products>.Success(created);
        }

        public async Task<Response<bool>> CreateProductListAsync(Products prod)
        {
            if (string.IsNullOrEmpty(prod.Name))
                 return Response<bool>.Failure(Status.Empty);
            
            var parameters = new
            {
                prod.Name,
                prod.Details,
                prod.Active,
                prod.Price,
                DateCreate = DateTime.Now
            };
            int result = await _conn.ExecuteAsync(ProductSqlQuery.QueryCreateProduct, parameters);
            if(result is 0)
                return Response<bool>.Failure(Status.InsertFailure);
            
            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteProductAsync(int id)
        {
            var parameters = new { id };
            var delete = await _conn.ExecuteAsync(ProductSqlQuery.QueryDeleteProduct, parameters);
            if(delete is 0)
                return Response<bool>.Failure(Status.DeleteFailure);
            
            return Response<bool>.Success(true, Status.DeletedSuccess);
        }

        public async Task<Response<Products>> GetByIdProductAsync(int id)
        {
            var parameters = new { id };
            var product = await _conn.QueryFirstOrDefaultAsync<Products>(ProductSqlQuery.QueryGetByIdProduct, parameters);
            if(product is null)
                return Response<Products>.Failure(Status.noDatafound);

            return Response<Products>.Success(product);
        }

        public async Task<Products> GetByNameProductAsync(string name)
        {
            var parameters = new { name };
            var product = await _conn.QueryFirstOrDefaultAsync<Products>(ProductSqlQuery.QueryGetByNameProduct, parameters);
            return product!;
        }

        public Task<Products> GetByProductsParametersAsync(Products prod)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Products>> GetProductsAsync()
        {
            var products = await _conn.QueryAsync<Products>(ProductSqlQuery.QuerySelectProduct);
            if (!products.Any())
                return Response<Products>.Failure(Status.noDatafound);

            return Response<Products>.Success(products.ToArray());
        }

        public async Task<Response<Products>> UpdateProductAsync(Products prod)
        {
            var parameters = new
            {
                prod.Id,
                prod.Name,
                prod.Details,
                prod.Active,
                prod.Price,
                DateEdit = DateTime.Now
            };

            var update = await _conn.ExecuteAsync(ProductSqlQuery.QueryUpdateProduct, parameters);
             if(update is 0)
                return Response<Products>.Failure(Status.UpdateFailure);

            var updated = await _conn.QueryFirstOrDefaultAsync<Products>(ProductSqlQuery.QueryGetByIdProduct, new { prod.Id });
            return Response<Products>.Success(updated!, Status.UpdatedSuccess);
        }
    }
}
