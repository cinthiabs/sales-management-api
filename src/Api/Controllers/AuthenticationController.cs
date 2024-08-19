using Api.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IMapper mapper, IAuthentication authentication) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthentication _authentication = authentication;

        [HttpPost("Authentication")]
        [ProducesResponseType(typeof(Response<UserCredentialsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationAsync([FromBody] LoginDto loginDto)
        {
            var login = _mapper.Map<Login>(loginDto);
            var user = await _authentication.AuthenticationAsync(login);
            if (user.IsFailure)
                return BadRequest(user);

            var userAuth = _mapper.Map<UserCredentialsDto>(user.Data.FirstOrDefault());
            return Ok(Response<UserCredentialsDto>.Success(userAuth));
        }
    }
}