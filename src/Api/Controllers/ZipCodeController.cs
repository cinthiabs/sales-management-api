using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ZipCodeController(IZipCode zipcode) : ControllerBase
    {
        private readonly IZipCode _zipcode = zipcode;

        [HttpGet("GetZipCode/{zipcode}")]
        [ProducesResponseType(typeof(Response<AddressDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetZipCodeAsync([FromRoute] string zipcode)
        {
            var user = await _zipcode.GetZipCodeAsync(zipcode);
            if (user.IsFailure && user.Code == Status.noDatafound )
                return NotFound(user);

            if (user.IsFailure)
                return BadRequest(user);

            return Ok(user);
        }
    }
}