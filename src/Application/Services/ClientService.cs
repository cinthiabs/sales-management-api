using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ClientService(IClientRepository clientRepository) : IClient
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        public async Task<Response<Clients>> CreateClientAsync(Clients client)
        {
            return  await _clientRepository.CreateClientAsync(client);
        }

        public async Task<Response<bool>> CreateClientListAsync(List<Clients> client)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteClientsAsync(int id)
        {
            var existClient = await _clientRepository.GetByIdClientAsync(id);
            if (existClient.IsSuccess)
            {
                var deleteClient = await _clientRepository.DeleteClientAsync(id);
                return deleteClient;
            }
            return Response<bool>.Failure(Status.noDatafound);
        }

        public async Task<Response<Clients>> GetByIdClientAsync(int id)
        {
            return await _clientRepository.GetByIdClientAsync(id);
        }

        public async Task<Clients> GetClientByNameAsync(string name)
        {
            return await _clientRepository.GetClientByNameAsync(name);
        }

        public async Task<Response<Clients>> GetClientsAsync()
        { 
            return await _clientRepository.GetClientsAsync();
        }

        public async Task<Response<Clients>> UpdateClientAsync(Clients client)
        {
             var existClient = await _clientRepository.GetByIdClientAsync(client.Id);
             if (existClient.IsSuccess)
             {
                 var updated = await _clientRepository.UpdateClientAsync(client);
                 return updated;
             }
            return existClient;
        }
    }
}