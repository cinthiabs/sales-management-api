using Core.Repositories;
using Dapper;
using Data.Infrastructure.Queries;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data.Infrastructure.Repository
{
    public class CostRepository : ICostRepository
    {
        private readonly SqlConnection _conn;
        public CostRepository(IConfiguration configuration) 
        {
            _conn = new SqlConnection();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _conn = new SqlConnection(connectionString);
        }

        public async Task<Costs> CreateCost(Costs cost)
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

            int id = await _conn.ExecuteAsync(CostSqlQuery.QueryCreateCost, parameters);
            cost.Id = id;
            return cost;
        }

        public async Task<bool> CreateCostList(Costs cost)
        {
            if (string.IsNullOrEmpty(cost.Name))
            {
                return false;
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
            return result > 0;
        }

        public async Task<bool> DeleteCost(int id)
        {
            var parameters = new { id };
            var delete = await _conn.ExecuteAsync(CostSqlQuery.QueryDeleteCost, parameters);
            return delete > 0;
        }

        public async Task<Costs> GetByIdCost(int id)
        {
            var parameters = new { id };
            var cost = await _conn.QueryFirstOrDefaultAsync<Costs>(CostSqlQuery.QueryGetByIdCost, parameters);
            return cost!;
        }

        public async Task<IEnumerable<Costs>> GetCosts()
        {
            var costs = await _conn.QueryAsync<Costs>(CostSqlQuery.QuerySelectCost);
            return costs;
        }

        public async Task<bool> UpdateCost(Costs cost)
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
            return update > 0;
        }
        public async Task<Costs> GetByCostsParameters(Costs cost)
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
    }
}
