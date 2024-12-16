using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [Authorize("Bearer")]
    public class ClientController(IClient client) : ApiController
    {
        private readonly IClient _client  = client;

        [HttpGet("GetAllClients")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var getClients = await _client.GetClientsAsync();
            return Response(getClients);
        }

        [HttpGet("GetByIdClient/{id}")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClientAsync(int id)
        {
            var getClientById = await _client.GetByIdClientAsync(id);
            return Response(getClientById);
        }

        [HttpPost("CreateClient")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClientAsync([FromBody] ClientDto clientDto)
        {
            var clientCreated = await _client.CreateClientAsync(clientDto);
            return Response(clientCreated);
        }

        [HttpPut("UpdateClient/{id}")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientDto clientDto, int id)
        {
            var clientUpdated = await _client.UpdateClientAsync(clientDto, id);
            return Response(clientUpdated);
        }

        [HttpDelete("DeleteClient/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var clientDelete = await _client.DeleteClientsAsync(id);
            return Response(clientDelete);
        }
        [HttpGet("GetRelClients")]
        [ProducesResponseType(typeof(Response<IEnumerable<RelClients>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRelClientsAsync(DateTime dateIni, DateTime dateEnd, int id = 0)
        {
            var clientDelete = await _client.GetRelClientsAsync(dateIni, dateEnd,id);
            return Response(clientDelete);
        }

    }
}