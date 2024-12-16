using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using sales_management_api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/v1")]
    [AllowAnonymous]
    [Authorize("Bearer")]
    public class AuthenticationController(IAuthentication authentication) : ApiController
    {
        private readonly IAuthentication _authentication = authentication;

        [HttpPost("Authentication")]
        [ProducesResponseType(typeof(Response<UserCredentialsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationAsync([FromBody] LoginDto loginDto)
        {
            var user = await _authentication.AuthenticationAsync(loginDto);
            return Response(user);
        }
    }
}