﻿using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infrastructure.Interfaces;
using Infrastructure.Queries;

namespace Infrastructure.Repositories
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

        public async Task<Costs> CreateCostAsync(Costs cost)
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

        public async Task<bool> CreateCostListAsync(Costs cost)
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

        public async Task<bool> DeleteCostAsync(int id)
        {
            var parameters = new { id };
            var delete = await _conn.ExecuteAsync(CostSqlQuery.QueryDeleteCost, parameters);
            return delete > 0;
        }

        public async Task<Costs> GetByIdCostAsync(int id)
        {
            var parameters = new { id };
            var cost = await _conn.QueryFirstOrDefaultAsync<Costs>(CostSqlQuery.QueryGetByIdCost, parameters);
            return cost!;
        }

        public async Task<IEnumerable<Costs>> GetCostsAsync()
        {
            var costs = await _conn.QueryAsync<Costs>(CostSqlQuery.QuerySelectCost);
            return costs;
        }

        public async Task<bool> UpdateCostAsync(Costs cost)
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
    }
}
