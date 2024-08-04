using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClient
    {
        Task<Clients> CreateClientAsync(Clients client);
        Task<bool> CreateClientListAsync(List<Clients> client);
        Task<bool> UpdateClientAsync(Clients client);
        Task<bool> DeleteClientsAsync(int id);
        Task<IEnumerable<Clients>> GetClientsAsync();
        Task<Clients> GetByIdClientAsync(int id);
    }
}