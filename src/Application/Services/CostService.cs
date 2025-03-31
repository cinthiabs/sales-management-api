using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Cache;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Net;

namespace Application.Services
{
    public class CostService(ICostRepository costRepository, ICacheService cacheService, IMapper mapper) : ICost
    {
        private readonly ICostRepository _costRepository = costRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Response<Costs>> CreateCostAsync(CostsDto costDto)
        {
            var mapCost = _mapper.Map<Costs>(costDto);
            return await _costRepository.CreateCostAsync(mapCost);
        }
        public async Task<bool> CreateCostListAsync(List<Costs> costDto)
        {
            var mapCost = _mapper.Map<List<Costs>>(costDto);

            foreach (var item in mapCost.Where(c => c.Name is not null))
            {
                var costExist = await _costRepository.GetByCostsParametersAsync(item);

                if (costExist.Id != 0)
                    continue;

                var data = await _costRepository.CreateCostListAsync(item);

                if (data.IsFailure)
                    return false;
            }
            return true;
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
            string key = "get_cost";
            
            var getCache = await _cacheService.GetAsync<Response<Costs>>(key);
            if(getCache is not null)
                return getCache;

            var getCost = await _costRepository.GetCostsAsync();
            await _cacheService.SetAsync(key, getCost);

            return getCost;
        }

        public async Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd)
        {
            return await _costRepository.GetRelCostPriceAsync(dateIni, dateEnd);
        }
        public async Task<Response<Costs>> UpdateCostAsync(CostsDto costDto, int id)
        {
            var mapCost = _mapper.Map<Costs>(costDto);
            mapCost.Id = id;

            var existCost = await _costRepository.GetByIdCostAsync(mapCost.Id);
            if (existCost.IsSuccess)
            {
                var updated = await _costRepository.UpdateCostAsync(mapCost);
                return updated;
            }
             return existCost;
        }
    }
}
