using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Dapper;
using Infrastructure.Queries;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class ClientRepository (IConfiguration configuration) : RepositoryBase(configuration),  IClientRepository
    {
        public  async Task<Response<Clients>> CreateClientAsync(Clients client)
        {
            var parameters = new
            {
                client.Name,
                client.Phone,
                client.Location,
                client.Active,
                DateCreate = DateTime.Now
            };

            var Id = await Connection.ExecuteScalarAsync(ClientSqlQuery.QueryCreateClient, parameters);
            var created = await Connection.QueryFirstOrDefaultAsync<Clients>(ClientSqlQuery.QueryGetByIdClient, new { Id });
            if (created is null)
                return Response<Clients>.Failure(Status.noDatafound);

            return Response<Clients>.Success(created);
        }

        public async Task<Response<bool>> CreateClientListAsync(Clients client)
        {
            if (string.IsNullOrEmpty(client.Name))
                return Response<bool>.Failure(Status.Empty);

            var parameters = new
            {
                client.Name,
                client.Phone,
                client.Location,
                client.Active,
                DateCreate = DateTime.Now
            };

            int result = await Connection.ExecuteAsync(ClientSqlQuery.QueryCreateClient, parameters);
            if(result is 0)
                return Response<bool>.Failure(Status.InsertFailure);
            
            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteClientAsync(int id)
        {
            var parameters = new { id };
            var delete = await Connection.ExecuteAsync(ClientSqlQuery.QueryDeleteClient, parameters);
            if(delete is 0)
                return Response<bool>.Failure(Status.DeleteFailure);
            
            return Response<bool>.Success(true, Status.DeletedSuccess);
        }

        public async Task<Response<Clients>> GetByIdClientAsync(int id)
        {
            var parameters = new { id };
            var client = await Connection.QueryFirstOrDefaultAsync<Clients>(ClientSqlQuery.QueryGetByIdClient, parameters);
            if(client is null)
                return Response<Clients>.Failure(Status.noDatafound);

            return Response<Clients>.Success(client);  
        }

        public async Task<Clients> GetClientByNameAsync(string name)
        {
           var parameters = new {name};
           var client = await Connection.QueryFirstOrDefaultAsync<Clients>(ClientSqlQuery.QueryGetClientByName, parameters);
            return client!;
        }

        public async Task<Response<Clients>> GetClientsAsync()
        {
            var clients = await Connection.QueryAsync<Clients>(ClientSqlQuery.QuerySelectClients);
            if (!clients.Any())
                return Response<Clients>.Failure(Status.noDatafound);

            return Response<Clients>.Success(clients.ToArray());
        }

        public async Task<Response<IEnumerable<RelClients>>> GetRelClientsAsync(DateTime dateIni, DateTime dateEnd, int id = 0)
        {
            var query = ClientSqlQuery.QuerySelectRelClients;
            if (id > 0)
                query += ClientSqlQuery.QuerySelectRelClientsById;

            if (dateIni != DateTime.MinValue && dateEnd != DateTime.MinValue)
                query += ClientSqlQuery.QuerySelectRelClientsByDate;
            
            var parameters = new
            {
                id = id > 0 ? id : (int?)null, 
                dateIni = dateIni != DateTime.MinValue ? dateIni : (DateTime?)null,
                dateEnd = dateEnd != DateTime.MinValue ? dateEnd : (DateTime?)null
            };
            var clients = await Connection.QueryAsync<RelClients>(query, parameters);

            if (clients == null || !clients.Any())
                return Response<IEnumerable<RelClients>>.Failure(Status.noDatafound);
            return Response<IEnumerable<RelClients>>.Success(clients);
        }


        public async Task<Response<Clients>> UpdateClientAsync(Clients client)
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

            var update = await Connection.ExecuteAsync(ClientSqlQuery.QueryUpdateClient, parameters);
            if(update is 0)
                return Response<Clients>.Failure(Status.UpdateFailure);

            var updated = await Connection.QueryFirstOrDefaultAsync<Clients>(ClientSqlQuery.QueryGetByIdClient, new { client.Id });
            return Response<Clients>.Success(updated!, Status.UpdatedSuccess);
        }
    }
}