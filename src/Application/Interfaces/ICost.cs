using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICost
    {
        Task<Costs> CreateCostAsync(Costs cost);
        Task<bool> CreateCostListAsync(List<Costs> cost);
        Task<bool> UpdateCostAsync(Costs cost);
        Task<bool> DeleteCostAsync(int id);
        Task<IEnumerable<Costs>> GetCostsAsync();
        Task<Costs> GetByIdCostAsync(int id);
        Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd);
    }
}
