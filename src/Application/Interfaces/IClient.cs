using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClient
    {
        Task<Response<Clients>> CreateClientAsync(ClientDto clientDto);
        Task<Response<bool>> CreateClientListAsync(List<ClientDto> clientDto);
        Task<Response<Clients>> UpdateClientAsync(ClientDto clientDto, int id);
        Task<Response<bool>> DeleteClientsAsync(int id);
        Task<Response<Clients>> GetClientsAsync();
        Task<Response<Clients>> GetByIdClientAsync(int id);
        Task<Clients> GetClientByNameAsync(string name);
        Task<Response<IEnumerable<RelClients>>> GetRelClientsAsync(DateTime dateIni, DateTime dateEnd, int id = 0);
    }
}