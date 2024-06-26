using Core.Services.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CostService : ICost
    {
        public Task<Costs> CreateCost(Sales cost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCostList(List<Costs> cost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Costs> GetByIdCost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Costs>> GetCosts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCost(Costs cost)
        {
            throw new NotImplementedException();
        }
    }
}
