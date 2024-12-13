using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using sales_management_api.Controllers;

namespace Api.Controllers
{
    [Route("api/v1")]
    public class ProfileController(IUserProfile userProfile) : ApiController
    {
        private readonly IUserProfile _userProfile = userProfile;

        [HttpGet("GetAllUserProfile")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserProfileAsync()
        {
            var getUserProfile = await _userProfile.GetAllUserProfileAsync();
            return Response(getUserProfile);
        }

        [HttpGet("GetByUsernameProfile/{Username}")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUsernameProfileAsync(string Username)
        {
            var profileId = await _userProfile.GetByUsernameProfileAsync(Username);
            return Response(profileId);
        }

        [HttpPut("UpdateUserProfile/{Username}")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserProfileDto userDto, string Username)
        {
            var userUpdated = await _userProfile.UpdateUserProfileAsync(userDto, Username);
            return Response(userUpdated);
        }
    }
}