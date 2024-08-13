using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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
        [ProducesResponseType(typeof(Response<ClientDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var getClients = await _client.GetClientsAsync();
            if (getClients.Code == Status.noDatafound)
                return NotFound(getClients);

            var clientsDto = _mapper.Map<IEnumerable<ClientDTO>>(getClients.Data);
            return Ok(clientsDto);
        }

        [HttpGet("GetByIdClient/{id}")]
        [ProducesResponseType(typeof(Response<ClientDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClientAsync(int id)
        {
            var getClientById = await _client.GetByIdClientAsync(id);
            if (getClientById.IsFailure)
                return NotFound(getClientById);
                
            var clientDto = _mapper.Map<ClientDTO>(getClientById.Data.FirstOrDefault());
            return Ok(clientDto);
        }
        
        [HttpPost("CreateClient")]
        [ProducesResponseType(typeof(Response<ClientDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClientAsync([FromBody] ClientDTO clientDto)
        {
            var mapClient = _mapper.Map<Clients>(clientDto);
            var clientCreated = await _client.CreateClientAsync(mapClient);
            if(clientCreated.IsFailure)
                return BadRequest(clientCreated);

            var clientCreatedDto = _mapper.Map<ClientDTO>(clientCreated.Data);
            return Ok(clientCreatedDto);
        }

        [HttpPut("UpdateClient/{id}")]
        [ProducesResponseType(typeof(Response<ClientDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientDTO clientDto, int id)
        {
            var mapClient = _mapper.Map<Clients>(clientDto);
            mapClient.Id = id;

            var clientUpdated = await _client.UpdateClientAsync(mapClient);

            if(clientUpdated.IsFailure)
                return BadRequest(clientUpdated);

            var clientDTO = _mapper.Map<ClientDTO>(clientUpdated.Data.FirstOrDefault());
            return Ok(Response<ClientDTO>.Success(clientDTO));
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