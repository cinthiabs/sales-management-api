using Core.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure.Repository
{
    public class CostRepository : ICostRepository
    {
        public Task<Costs> CreateCost(Costs cost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCostList(Costs cost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Costs> GetByCostsParameters(Costs cost)
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
