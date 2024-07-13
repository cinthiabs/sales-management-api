using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICostRepository
    {
        Task<Costs> CreateCostAsync(Costs cost);
        Task<bool> CreateCostListAsync(Costs cost);
        Task<bool> UpdateCostAsync(Costs cost);
        Task<bool> DeleteCostAsync(int id);
        Task<IEnumerable<Costs>> GetCostsAsync();
        Task<Costs> GetByIdCostAsync(int id);
        Task<Costs> GetByCostsParametersAsync(Costs cost);
        Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd);
    }
}
