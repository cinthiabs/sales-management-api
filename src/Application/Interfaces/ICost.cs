using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICost
    {
        Task<Response<Costs>> CreateCostAsync(CostsDto costDto);
        Task<bool> CreateCostListAsync(List<CostsDto> costDto);
        Task<Response<Costs>> UpdateCostAsync(CostsDto costDto, int id);
        Task<Response<bool>> DeleteCostAsync(int id);
        Task<Response<Costs>> GetCostsAsync();
        Task<Response<Costs>> GetByIdCostAsync(int id);
        Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd);
    }
}
