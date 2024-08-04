using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Infrastructure.Connection;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class CostRepository(IConfiguration configuration) : RepositoryBase(configuration), ICostRepository
    {
        public async Task<Response<Costs>> CreateCostAsync(Costs cost)
        {
            var parameters = new
            {
                cost.Quantity,
                cost.Name,
                cost.DateCost,
                cost.UnitPrice,
                cost.TotalPrice,
                DateCreate = DateTime.Now
            };

            var Id = await _conn.ExecuteScalarAsync(CostSqlQuery.QueryCreateCost, parameters);
            var created = await _conn.QueryFirstOrDefaultAsync<Costs>(CostSqlQuery.QueryGetByIdCost, new { Id } );
            if(created is null)
                return Response<Costs>.Failure(Status.noDatafound);
            
            return Response<Costs>.Success(created);
        }

        public async Task<Response<bool>> CreateCostListAsync(Costs cost)
        {
            if (string.IsNullOrEmpty(cost.Name))
            {
               return Response<bool>.Failure(Status.Empty);
            }

            var parameters = new
            {
                cost.Quantity,
                cost.Name,
                cost.DateCost,
                cost.UnitPrice,
                cost.TotalPrice,
                DateCreate = DateTime.Now
            };

            int result = await _conn.ExecuteAsync(CostSqlQuery.QueryCreateCost, parameters);
            if(result is 0)
                return Response<bool>.Failure(Status.InsertFailure);
            
            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteCostAsync(int id)
        {
            var parameters = new { id };
            var delete = await _conn.ExecuteAsync(CostSqlQuery.QueryDeleteCost, parameters);
            if(delete is 0)
                return Response<bool>.Failure(Status.DeleteFailure);

            return Response<bool>.Success(true, Status.DeletedSuccess);
        }

        public async Task<Response<Costs>> GetByIdCostAsync(int id)
        {
            var parameters = new { id };
            var cost = await _conn.QueryFirstOrDefaultAsync<Costs>(CostSqlQuery.QueryGetByIdCost, parameters);
            if(cost is null)
                return Response<Costs>.Failure(Status.noDatafound);
            
            return Response<Costs>.Success(cost);
        }

        public async Task<Response<Costs>> GetCostsAsync()
        {
            var costs = await _conn.QueryAsync<Costs>(CostSqlQuery.QuerySelectCost);
            if(!costs.Any())
                return Response<Costs>.Failure(Status.noDatafound);

            return Response<Costs>.Success(costs.ToArray());
        }

        public async Task<Response<Costs>> UpdateCostAsync(Costs cost)
        {
            var parameters = new
            {
                cost.Id,
                cost.Quantity,
                cost.Name,
                cost.DateCost,
                cost.UnitPrice,
                cost.TotalPrice,
                DateEdit = DateTime.Now
            };

            var update = await _conn.ExecuteAsync(CostSqlQuery.QueryUpdateCost, parameters);
            if(update is 0)
                return Response<Costs>.Failure(Status.UpdateFailure);
            
            var updated = await _conn.QueryFirstOrDefaultAsync<Costs>(CostSqlQuery.QueryGetByIdCost, new { cost.Id });
            return Response<Costs>.Success(updated!, Status.UpdatedSuccess);
        }
        public async Task<Costs> GetByCostsParametersAsync(Costs cost)
        {
            var parameters = new
            {
                cost.Quantity,
                cost.Name,
                cost.DateCost,
                cost.UnitPrice
            };

            var result = await _conn.QueryFirstOrDefaultAsync<Costs>(CostSqlQuery.QueryByCostParameters, parameters);
            return result ?? new Costs { Name = "" };
        }
        public async Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd)
        {
            var parameters = new
            {
                dateIni,
                dateEnd
            };
            var rel = await _conn.QueryAsync<RelPriceCost>(CostSqlQuery.GetRelCostPrice, parameters);
            return rel!;
        }
    }
}
