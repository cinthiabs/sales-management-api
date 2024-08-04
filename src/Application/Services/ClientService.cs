using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ClientService(IClientRepository clientRepository) : IClient
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        public async Task<Clients> CreateClientAsync(Clients client)
        {
            var result = await _clientRepository.CreateClientAsync(client);
            return result;
        }

        public async Task<bool> CreateClientListAsync(List<Clients> client)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteClientsAsync(int id)
        {
            var existClient = await _clientRepository.GetByIdClientAsync(id);
            if (existClient is not null)
            {
                var deleteClient = await _clientRepository.DeleteClientAsync(id);
                return deleteClient;
            }
            return false;
        }

        public async Task<Clients> GetByIdClientAsync(int id)
        {
            return await _clientRepository.GetByIdClientAsync(id);
        }

        public async Task<IEnumerable<Clients>> GetClientsAsync()
        { 
            return await _clientRepository.GetClientsAsync();
        }

        public async Task<bool> UpdateClientAsync(Clients client)
        {
             var record = await _clientRepository.GetByIdClientAsync(client.Id);
                if (record is not null)
                {
                    var updated = await _clientRepository.UpdateClientAsync(client);
                    return updated;
                }
            return false;
        }
    }
}