using Entities.Entities;

namespace Core.Services.Interfaces
{
    public interface ICost
    {
        Task<Costs> CreateCost(Sales cost);
        Task<bool> CreateCostList(List<Costs> cost);
        Task<bool> UpdateCost(Costs cost);
        Task<bool> DeleteCost(int id);
        Task<IEnumerable<Costs>> GetCosts();
        Task<Costs> GetByIdCost(int id);
    }
}
