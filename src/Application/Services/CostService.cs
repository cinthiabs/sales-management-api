using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class CostService(ICostRepository costRepository) : ICost
    {
        private readonly ICostRepository _costRepository = costRepository;

        public async Task<Response<Costs>> CreateCostAsync(Costs cost)
        {
            return await _costRepository.CreateCostAsync(cost);
        }
        public async Task<bool> CreateCostListAsync(List<Costs> costs)
        {
            bool result = true;

            foreach (var item in costs)
            {
                if (item.Name is not null)
                {
                    var costExist = await _costRepository.GetByCostsParametersAsync(item);

                    if (costExist.Id == 0)
                    {
                        var data = await _costRepository.CreateCostListAsync(item);
                        
                        if(data.IsFailure)
                            return result = false;

                        result = data.IsSuccess;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public async Task<Response<bool>> DeleteCostAsync(int id)
        {
            var existCost = await _costRepository.GetByIdCostAsync(id);
            if (existCost.IsSuccess)
            {
                var deleteCost = await _costRepository.DeleteCostAsync(id);
                return deleteCost;
            }
            return Response<bool>.Failure(Status.noDatafound);
        }
        public async Task<Response<Costs>> GetByIdCostAsync(int id)
        {
            return await _costRepository.GetByIdCostAsync(id);
        }

        public async Task<Response<Costs>> GetCostsAsync()
        {
            return await _costRepository.GetCostsAsync();
        }

        public async Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd)
        {
            return await _costRepository.GetRelCostPriceAsync(dateIni, dateEnd);
        }

        public async Task<Response<Costs>> UpdateCostAsync(Costs cost)
        {
            var existCost = await _costRepository.GetByIdCostAsync(cost.Id);
            if (existCost.IsSuccess)
            {
                var updated = await _costRepository.UpdateCostAsync(cost);
                return updated;
            }
             return existCost;
        }
    }
}
