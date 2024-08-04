using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICost
    {
        Task<Response<Costs>> CreateCostAsync(Costs cost);
        Task<bool> CreateCostListAsync(List<Costs> cost);
        Task<Response<Costs>> UpdateCostAsync(Costs cost);
        Task<Response<bool>> DeleteCostAsync(int id);
        Task<Response<Costs>> GetCostsAsync();
        Task<Response<Costs>> GetByIdCostAsync(int id);
        Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd);
    }
}
