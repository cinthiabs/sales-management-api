using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using sales_management_api.Controllers;

namespace Api.Controllers
{
    [Route("api/v1")]
    public class UserCredentialsController(IUser user) : ApiController
    {
        private readonly IUser _user = user;

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([FromBody] LoginDto loginDto)
        {
            var userCreated = await _user.CreateUserAsync(loginDto);
            return Response(userCreated);
        }
    }
}