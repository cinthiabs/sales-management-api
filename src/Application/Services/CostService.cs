using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class CostService : ICost
    {
        private readonly ICostRepository _costRepository;
        public CostService(ICostRepository costRepository)
        {
            _costRepository = costRepository;
        }
            
        public async Task<Costs> CreateCost(Costs cost)
        {
            var result = await _costRepository.CreateCost(cost);
            return result;
        }


        public async Task<bool> CreateCostList(List<Costs> costs)
        {
            bool result = false;

            foreach (var item in costs)
            {
                if (item.Name is not null)
                {
                    var costExist = await _costRepository.GetByCostsParameters(item);

                    if (costExist.Id == 0)
                    {
                        result = await _costRepository.CreateCostList(item);
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public async Task<bool> DeleteCost(int id)
        {
            var record = await _costRepository.GetByIdCost(id);
            if (record is not null)
            {
                var rowsAffected = await _costRepository.DeleteCost(id);
                return rowsAffected;
            }
            return false;
        }

        public async Task<Costs> GetByIdCost(int id)
        {
            var cost = await _costRepository.GetByIdCost(id);
            return cost;
        }

        public async Task<IEnumerable<Costs>> GetCosts()
        {
            var costs = await _costRepository.GetCosts();
            return costs;
        }

        public async Task<bool> UpdateCost(Costs cost)
        {
            var record = await _costRepository.GetByIdCost(cost.Id);
            if (record is not null)
            {
                var updated = await _costRepository.UpdateCost(cost);
                return updated;
            }
            return false;
        }

    }
}
