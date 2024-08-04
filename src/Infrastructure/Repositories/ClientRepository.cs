using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Dapper;
using Infrastructure.Queries;

namespace Infrastructure.Repositories
{
    public class ClientRepository (IConfiguration configuration) : RepositoryBase(configuration),  IClientRepository
    {
        public  async Task<Clients> CreateClientAsync(Clients client)
        {
            var parameters = new
            {
                client.Name,
                client.Phone,
                client.Location,
                client.Active,
                DateCreate = DateTime.Now
            };

            var Id = await _conn.ExecuteScalarAsync(ClientSqlQuery.QueryCreateClient, parameters);
            var created = await _conn.QueryFirstOrDefaultAsync<Clients>(ClientSqlQuery.QueryGetByIdClient, new { Id });
            return created!;
        }

        public async Task<bool> CreateClientListAsync(Clients client)
        {
             if (string.IsNullOrEmpty(client.Name))
            {
                return false;
            }

            var parameters = new
            {
                client.Name,
                client.Phone,
                client.Location,
                client.Active,
                DateCreate = DateTime.Now
            };

            int result = await _conn.ExecuteAsync(ClientSqlQuery.QueryCreateClient, parameters);
            return result > 0;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
             var parameters = new { id };
            var delete = await _conn.ExecuteAsync(ClientSqlQuery.QueryDeleteClient, parameters);
            return delete > 0;
        }

        public async Task<Clients> GetByIdClientAsync(int id)
        {
            var parameters = new { id };
            var client = await _conn.QueryFirstOrDefaultAsync<Clients>(ClientSqlQuery.QueryGetByIdClient, parameters);
            return client!;
        }

        public async Task<IEnumerable<Clients>> GetClientsAsync()
        {
            var clients = await _conn.QueryAsync<Clients>(ClientSqlQuery.QuerySelectClients);
            return clients;
        }

        public async Task<bool> UpdateClientAsync(Clients client)
        {
           var parameters = new
            {
                client.Id,
                client.Name,
                client.Phone,
                client.Location,
                client.Active,
                DateEdit = DateTime.Now
            };

            var update = await _conn.ExecuteAsync(ClientSqlQuery.QueryUpdateClient, parameters);
            return update > 0;
        }
    }
}