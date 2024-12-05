using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserCredentialsController(IUser user) : ControllerBase
    {
        private readonly IUser _user = user;

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([FromBody] LoginDto loginDto)
        {
            var userCreated = await _user.CreateUserAsync(loginDto);

            if (userCreated.IsFailure && userCreated.Code == Status.ConflitUser)
                return Conflict(user);

            if (userCreated.IsFailure)
                return BadRequest(userCreated);

            return Ok(userCreated);
        }
    }
}