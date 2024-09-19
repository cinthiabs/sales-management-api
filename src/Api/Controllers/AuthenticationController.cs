using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthenticationController(IAuthentication authentication) : ControllerBase
    {
        private readonly IAuthentication _authentication = authentication;

        [HttpPost("Authentication")]
        [ProducesResponseType(typeof(Response<UserCredentialsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationAsync([FromBody] LoginDto loginDto)
        {
            var user = await _authentication.AuthenticationAsync(loginDto);
            if (user.IsFailure && user.Code == Status.noDatafound )
                return NotFound(user);

            if (user.IsFailure)
                return BadRequest(user);

            return Ok(user);
        }
    }
}