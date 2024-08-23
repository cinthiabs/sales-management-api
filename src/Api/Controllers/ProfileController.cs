using Api.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ProfileController(IMapper mapper, IUserProfile userProfile) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
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

        [HttpGet("GetByIdUserProfile/{id}")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdUserProfileAsync(int id)
        {
            var profileId = await _userProfile.GetByIdUserProfileAsync(id);
            if (profileId.IsFailure)
                return NotFound(profileId);

            return Ok(profileId);
        }

        [HttpPut("UpdateUserProfile/{id}")]
        [ProducesResponseType(typeof(Response<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserProfileDto userDto, int id)
        {
            var mapUser = _mapper.Map<UserProfile>(userDto);
            mapUser.Id = id;

            var userUpdated = await _userProfile.UpdateUserProfileAsync(mapUser);
            if (userUpdated.IsFailure)
                return BadRequest(userUpdated);

            return Ok(userUpdated);
        }
    }
}