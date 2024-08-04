using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IClientRepository
    {
        Task<Response<Clients>> CreateClientAsync(Clients client);
        Task<Response<bool>> CreateClientListAsync(Clients client);
        Task<Response<Clients>> UpdateClientAsync(Clients client);
        Task<Response<bool>> DeleteClientAsync(int id);
        Task<Response<Clients>> GetClientsAsync();
        Task<Response<Clients>> GetByIdClientAsync(int id);
    }
}