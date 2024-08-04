using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICostRepository
    {
        Task<Response<Costs>> CreateCostAsync(Costs cost);
        Task<Response<bool>> CreateCostListAsync(Costs cost);
        Task<Response<Costs>> UpdateCostAsync(Costs cost);
        Task<Response<bool>> DeleteCostAsync(int id);
        Task<Response<Costs>> GetCostsAsync();
        Task<Response<Costs>> GetByIdCostAsync(int id);
        Task<Costs> GetByCostsParametersAsync(Costs cost);
        Task<IEnumerable<RelPriceCost>> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd);
    }
}
