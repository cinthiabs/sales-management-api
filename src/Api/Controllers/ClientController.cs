using Api.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class ClientController(IClient client, IMapper mapper) : ControllerBase
    {
        private readonly IClient _client  = client;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllClients")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var getClients = await _client.GetClientsAsync();
            if (getClients.Code == Status.noDatafound)
                return NotFound(getClients);

            return Ok(getClients);
        }

        [HttpGet("GetByIdClient/{id}")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClientAsync(int id)
        {
            var getClientById = await _client.GetByIdClientAsync(id);
            if (getClientById.IsFailure)
                return NotFound(getClientById);
                
            return Ok(getClientById);
        }
        
        [HttpPost("CreateClient")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClientAsync([FromBody] ClientDto clientDto)
        {
            var mapClient = _mapper.Map<Clients>(clientDto);
            var clientCreated = await _client.CreateClientAsync(mapClient);
            if(clientCreated.IsFailure)
                return BadRequest(clientCreated);

            return Ok(clientCreated);
        }

        [HttpPut("UpdateClient/{id}")]
        [ProducesResponseType(typeof(Response<Clients>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientDto client, int id)
        {
            var mapClient = _mapper.Map<Clients>(client);
            mapClient.Id = id;

            var clientUpdated = await _client.UpdateClientAsync(mapClient);

            if(clientUpdated.IsFailure)
                return BadRequest(clientUpdated);

            return Ok(clientUpdated);
        }

        [HttpDelete("DeleteClient/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
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