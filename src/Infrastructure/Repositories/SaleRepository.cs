﻿using Dapper;
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
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.DateSale,
                @DateCreate = DateTime.Now
            };

            var Id = await _conn.ExecuteScalarAsync(SaleSqlQuery.QueryCreateSale, parameters);
            var created = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, new { Id });
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
               sale.IdProduct,
               sale.Name,
               sale.Price,
               sale.Details,
               sale.Quantity,
               sale.DateSale,
               sale.Pay,
               DateCreate = DateTime.Now
           };
           int result = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);
           if(result is 0)
                return Response<bool>.Failure(Status.InsertFailure);
            
            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteSaleAsync(int id)
        {
            var parameters = new { id };
            var  delete = await _conn.ExecuteAsync(SaleSqlQuery.QueryDeleteSale, parameters);
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
            var sale = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, parameters);
            if(sale is null)
                return Response<Sales>.Failure(Status.noDatafound);
            
            return Response<Sales>.Success(sale);
        }

        public async Task<Response<Sales>> GetSalesAsync()
        {
            var sale = await _conn.QueryAsync<Sales>(SaleSqlQuery.QuerySelectSale);
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
                sale.DateSale,
                @DateEdit = DateTime.Now
                
            };

            var update = await _conn.ExecuteAsync(SaleSqlQuery.QueryUpdateSale, parameters);
            if(update is 0)
                return Response<Sales>.Failure(Status.UpdateFailure);
            
            var updated = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, new { sale.Id });
            return Response<Sales>.Success(updated!, Status.UpdatedSuccess);

        }

        public async Task<Sales> GetBySaleParametersAsync(Sales sale)
        {
            var parameters = new
            {
                sale.Name,
                sale.Price,
                Details = string.IsNullOrEmpty(sale.Details) ? (object)DBNull.Value : sale.Details,
                sale.Quantity,
                sale.DateSale
            };
            var result = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryBySaleParameters, parameters);
            return result ?? new Sales { Name = "" };
        }

        public async Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var parameters = new
            {
                dateIni,
                dateEnd
            };
            var rel = await _conn.QueryAsync<RelQuantity>(SaleSqlQuery.GetRelQuantity, parameters);
            return rel!;
        }
    }
}
