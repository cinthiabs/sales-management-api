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
            if (clients.IsFailure)
                return BadRequest(clients);

            var clientsDto = _mapper.Map<IEnumerable<ClientDTO>>(clients.Data);
            return Ok(clientsDto);

        }

        [HttpGet("GetByIdClient/{id}")]
        [ProducesResponseType(typeof(ClientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClientAsync(int id)
        {
            var client = await _client.GetByIdClientAsync(id);
            if (client.IsFailure)
                return NotFound(client);
                
            var clientsDto = _mapper.Map<ClientDTO>(client.Data.FirstOrDefault());
            return Ok(clientsDto);
        }
        
        [HttpPost("CreateClient")]
        [ProducesResponseType(typeof(ClientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClientAsync([FromBody] ClientDTO clientDto)
        {
            var client = _mapper.Map<Clients>(clientDto);
            var clientCreated = await _client.CreateClientAsync(client);
            if(clientCreated.IsFailure)
                return BadRequest(clientCreated);

            var clientCreatedDto = _mapper.Map<ClientDTO>(clientCreated.Data);
            return Ok(clientCreatedDto);
        }

        [HttpPut("UpdateClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientDTO clientDto, int id)
        {
            var client = _mapper.Map<Clients>(clientDto);
            client.Id = id;

            var clientUpdated = await _client.UpdateClientAsync(client);

            if(clientUpdated.IsFailure)
                return BadRequest(clientUpdated);

            return Ok(clientUpdated);
        }

        [HttpDelete("DeleteClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var clientDelete = await _client.DeleteClientsAsync(id);
            if(clientDelete.IsFailure) 
                return BadRequest(clientDelete);
            
            return  Ok(clientDelete);
        }
        
    }
}