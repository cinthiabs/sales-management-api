using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClient
    {
        Task<Response<Clients>> CreateClientAsync(Clients client);
        Task<Response<bool>> CreateClientListAsync(List<Clients> client);
        Task<Response<Clients>> UpdateClientAsync(Clients client);
        Task<Response<bool>> DeleteClientsAsync(int id);
        Task<Response<Clients>> GetClientsAsync();
        Task<Response<Clients>> GetByIdClientAsync(int id);
        Task<Clients> GetClientByNameAsync(string name);
    }
}