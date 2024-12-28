using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class SaleRepository(IConfiguration configuration) : RepositoryBase(configuration), ISaleRepository
    {   
        public async Task<Response<Sales>> CreateSaleAsync(Sales sale)
        {
            var parameters = new
            {
                IdProduct = sale.IdProduct.HasValue ? (int?)sale.IdProduct : null,
                IdClient = sale.IdClient.HasValue ? (int?)sale.IdClient : null,
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.DateSale,
                sale.Pay,
                @DateCreate = DateTime.Now
            };

            var Id = await Connection.ExecuteScalarAsync(SaleSqlQuery.QueryCreateSale, parameters);
            var created = await Connection.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, new { Id });
            if (created is null)
                return Response<Sales>.Failure(Status.noDatafound);
            
            return Response<Sales>.Success(created);
        }

        public async Task<Response<bool>> CreateSaleListAsync(Sales sale)
        {
           if (string.IsNullOrEmpty(sale.Name))
              return Response<bool>.Failure(Status.Empty);
          
           var parameters = new
           {
               IdProduct = sale.IdProduct.HasValue ? (int?)sale.IdProduct : null,
               IdClient = sale.IdClient.HasValue ? (int?)sale.IdClient : null,
               sale.Name,
               sale.Price,
               sale.Details,
               sale.Quantity,
               sale.DateSale,
               sale.Pay,
               DateCreate = DateTime.Now
           };
           int result = await Connection.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);
           if(result is 0)
                return Response<bool>.Failure(Status.InsertFailure);
            
            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteSaleAsync(int id)
        {
            var parameters = new { id };
            var  delete = await Connection.ExecuteAsync(SaleSqlQuery.QueryDeleteSale, parameters);
             if(delete is 0)
                return Response<bool>.Failure(Status.DeleteFailure);
            
            return Response<bool>.Success(true, Status.DeletedSuccess);
        }

        public Task<IEnumerable<Sales>> GetByFiltersAsync(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Sales>> GetByIdSaleAsync(int id)
        {
            var parameters = new { id };
            var sale = await Connection.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, parameters);
            if(sale is null)
                return Response<Sales>.Failure(Status.noDatafound);
            
            return Response<Sales>.Success(sale);
        }

        public async Task<Response<Sales>> GetSalesAsync()
        {
            var sale = await Connection.QueryAsync<Sales>(SaleSqlQuery.QuerySelectSale);
            if(!sale.Any())
                return Response<Sales>.Failure(Status.noDatafound);

            return Response<Sales>.Success(sale.ToArray());
        }

        public async Task<Response<Sales>> UpdateSaleAsync(Sales sale)
        {
            var parameters = new 
            {
                sale.Id,
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.Pay,
                sale.IdProduct,
                sale.IdClient,
                sale.DateSale,
                @DateEdit = DateTime.Now
                
            };

            var update = await Connection.ExecuteAsync(SaleSqlQuery.QueryUpdateSale, parameters);
            if(update is 0)
                return Response<Sales>.Failure(Status.UpdateFailure);
            
            var updated = await Connection.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, new { sale.Id });
            return Response<Sales>.Success(updated!, Status.UpdatedSuccess);

        }

        public async Task<Sales> GetBySaleParametersAsync(Sales sale)
        {
            var parameters = new
            {
                sale.Name,
                sale.Price,
                IdProduct = sale.IdProduct.HasValue ? (object)sale.IdProduct.Value : DBNull.Value,
                sale.Quantity,
                sale.Details,
                sale.DateSale
            };

            string query = SaleSqlQuery.QueryBySaleParameters;

            if (string.IsNullOrEmpty(sale.Details))
                query += " AND (Details IS NULL)";
            else
                query += " AND Details = @Details";

            var result = await Connection.QueryFirstOrDefaultAsync<Sales>(query, parameters);
            return result ?? new Sales { Name = "" };
        }

        public async Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var parameters = new
            {
                dateIni,
                dateEnd
            };
            var rel = await Connection.QueryAsync<RelQuantity>(SaleSqlQuery.GetRelQuantity, parameters);
            return rel!;
        }
    }
}
