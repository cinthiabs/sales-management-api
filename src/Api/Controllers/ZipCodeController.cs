using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sales_management_api.Controllers;

namespace Api.Controllers
{
    [Route("api/v1")]
    [Authorize("Bearer")]

    public class ZipCodeController(IZipCode zipcode) : ApiController
    {
        private readonly IZipCode _zipcode = zipcode;

        [HttpGet("GetZipCode/{zipcode}")]
        [ProducesResponseType(typeof(Response<AddressDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetZipCodeAsync([FromRoute] string zipcode)
        {
            var zip = await _zipcode.GetZipCodeAsync(zipcode);
            return Response(zip);
        }
    }
}