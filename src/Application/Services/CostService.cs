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
            
        public async Task<Costs> CreateCostAsync(Costs cost)
        {
            var result = await _costRepository.CreateCostAsync(cost);
            return result;
        }


        public async Task<bool> CreateCostListAsync(List<Costs> costs)
        {
            bool result = false;

            foreach (var item in costs)
            {
                if (item.Name is not null)
                {
                    var costExist = await _costRepository.GetByCostsParametersAsync(item);

                    if (costExist.Id == 0)
                    {
                        result = await _costRepository.CreateCostListAsync(item);
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public async Task<bool> DeleteCostAsync(int id)
        {
            var record = await _costRepository.GetByIdCostAsync(id);
            if (record is not null)
            {
                var rowsAffected = await _costRepository.DeleteCostAsync(id);
                return rowsAffected;
            }
            return false;
        }

        public async Task<Costs> GetByIdCostAsync(int id)
        {
            var cost = await _costRepository.GetByIdCostAsync(id);
            return cost;
        }

        public async Task<IEnumerable<Costs>> GetCostsAsync()
        {
            var costs = await _costRepository.GetCostsAsync();
            return costs;
        }

        public async Task<bool> UpdateCostAsync(Costs cost)
        {
            var record = await _costRepository.GetByIdCostAsync(cost.Id);
            if (record is not null)
            {
                var updated = await _costRepository.UpdateCostAsync(cost);
                return updated;
            }
            return false;
        }

    }
}
