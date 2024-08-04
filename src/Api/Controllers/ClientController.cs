using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ClientController(IClient client, IMapper mapper) : ControllerBase
    {
        private readonly IClient _client  = client;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllClients")]
        [ProducesResponseType(typeof(IEnumerable<ClientDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var clients = await _client.GetClientsAsync();
            var clientsDto = _mapper.Map<IEnumerable<ClientDTO>>(clients);
            return Ok(clientsDto);
        }

        [HttpGet("GetByIdClient/{id}")]
        [ProducesResponseType(typeof(ClientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClientAsync(int id)
        {
            if (id == 0)
                return BadRequest("Invalid Client");

            var client = await _client.GetByIdClientAsync(id);
            if (client is null)
                return NotFound();

            var clientsDto = _mapper.Map<ClientDTO>(client);
            return Ok(clientsDto);
        }
        
        [HttpPost("CreateClient")]
        [ProducesResponseType(typeof(ClientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClientAsync([FromBody] ClientDTO clientDto)
        {
            if (clientDto is null)
                return BadRequest("Client invalid!");

            var client = _mapper.Map<Clients>(clientDto);
            var clientCreated = await _client.CreateClientAsync(client);
            var clientCreatedDto = _mapper.Map<ClientDTO>(clientCreated);
            return Ok(clientCreatedDto);
        }

        [HttpPut("UpdateClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientDTO clientDto, int id)
        {
            if (clientDto is null)
                return BadRequest("Client invalid!");

            var client = _mapper.Map<Clients>(clientDto);
            client.Id = id;
            bool clientUpdated = await _client.UpdateClientAsync(client);
            return clientUpdated ? Ok(clientUpdated) : BadRequest("Unable to update data!");
        }

        [HttpDelete("DeleteClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            if (id == 0 )
                return BadRequest("Client invalid!");
            bool clientDelete = await _client.DeleteClientsAsync(id);
            return clientDelete ? Ok(clientDelete) : BadRequest("Unable to delete data!");
        }
        
    }
}