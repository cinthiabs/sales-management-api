using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IClientRepository
    {
         Task<Clients> CreateClientAsync(Clients client);
        Task<bool> CreateClientListAsync(Clients client);
        Task<bool> UpdateClientAsync(Clients client);
        Task<bool> DeleteClientAsync(int id);
        Task<IEnumerable<Clients>> GetClientsAsync();
        Task<Clients> GetByIdClientAsync(int id);
    }
}