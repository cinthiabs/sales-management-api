using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Cache;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ClientService(IClientRepository clientRepository, ICacheService cacheService, IMapper mapper) : IClient
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Response<Clients>> CreateClientAsync(ClientDto clientDto)
        {
            var mapClient = _mapper.Map<Clients>(clientDto);
            return await _clientRepository.CreateClientAsync(mapClient);
        }

        public async Task<Response<bool>> CreateClientListAsync(List<ClientDto> client)
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
            var getCache = await _cacheService.GetAsync<Response<Clients>>(id.ToString());
            if (getCache != null)
            {
                return getCache;
            }
            var getClient = await _clientRepository.GetByIdClientAsync(id);
            await _cacheService.SetAsync(id.ToString(), getClient);

            return getClient;
        }
        public async Task<Clients> GetClientByNameAsync(string name)
        {
            return await _clientRepository.GetClientByNameAsync(name);
        }

        public async Task<Response<Clients>> GetClientsAsync()
        { 
            return await _clientRepository.GetClientsAsync();
        }

        public async Task<Response<IEnumerable<RelClients>>> GetRelClientsAsync(DateTime dateIni, DateTime dateEnd, int id = 0)
        {
            return await _clientRepository.GetRelClientsAsync(dateIni, dateEnd, id);
        }

        public async Task<Response<Clients>> UpdateClientAsync(ClientDto clientDto, int id)
        {
            var mapClient = _mapper.Map<Clients>(clientDto);
            mapClient.Id = id;

            var existClient = await _clientRepository.GetByIdClientAsync(mapClient.Id);
             if (existClient.IsSuccess)
             {
                 var updated = await _clientRepository.UpdateClientAsync(mapClient);
                 return updated;
             }
            return existClient;
        }
    }
}