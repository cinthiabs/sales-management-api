using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserCredentialsController(IUser user, IMapper mapper) : ControllerBase
    {
        private readonly IUser _user = user;
        private readonly IMapper _mapper = mapper;

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(LoginDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([FromBody] LoginDTO loginDto)
        {
            var login = _mapper.Map<Login>(loginDto);
            var userCreated = await _user.CreateUserAsync(login);
            if (userCreated.IsFailure)
                return BadRequest(userCreated);

            var userCreatedDto = _mapper.Map<LoginDTO>(userCreated.Data);
            return Ok(userCreatedDto);
        }
    }
}