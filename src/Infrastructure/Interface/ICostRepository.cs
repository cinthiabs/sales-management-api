using Domain.Entities;

namespace Core.Repositories
{
    public interface ICostRepository
    {
        Task<Costs> CreateCost(Costs cost);
        Task<bool> CreateCostList(Costs cost);
        Task<bool> UpdateCost(Costs cost);
        Task<bool> DeleteCost(int id);
        Task<IEnumerable<Costs>> GetCosts();
        Task<Costs> GetByIdCost(int id);
        Task<Costs> GetByCostsParameters(Costs cost);
    }
}
