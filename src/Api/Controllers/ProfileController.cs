using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProfileController(IUserProfile userProfile) : ControllerBase
    {
        private readonly IUserProfile _userProfile = userProfile;

        [HttpGet("GetAllUserProfile")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserProfileAsync()
        {
            var getUserProfile = await _userProfile.GetAllUserProfileAsync();
            if (getUserProfile.Code == Status.noDatafound)
                return NotFound(getUserProfile);

            return Ok(getUserProfile);
        }

        [HttpGet("GetByUsernameProfile/{Username}")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUsernameProfileAsync(string Username)
        {
            var profileId = await _userProfile.GetByUsernameProfileAsync(Username);
            if (profileId.IsFailure)
                return NotFound(profileId);

            return Ok(profileId);
        }

        [HttpPut("UpdateUserProfile/{Username}")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserProfileDto userDto, string Username)
        {

            var userUpdated = await _userProfile.UpdateUserProfileAsync(userDto, Username);
            if (userUpdated.IsFailure)
                return BadRequest(userUpdated);

            return Ok(userUpdated);
        }
    }
}