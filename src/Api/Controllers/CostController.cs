using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class CostController(ICost cost) : ControllerBase
    {
        private readonly ICost _cost = cost;

        [HttpGet("GetAllCosts")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCostsAsync()
        {
            var getCosts = await _cost.GetCostsAsync();
            if(getCosts.Code == Status.noDatafound)
                return NotFound(getCosts);

            if (getCosts.IsFailure)
                return BadRequest(getCosts);
            
            return Ok(getCosts);
        }

        [HttpGet("GetByIdCost/{id}")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdCostAsync(int id)
        {
            var getCostId = await _cost.GetByIdCostAsync(id);

            if (getCostId.Code == Status.noDatafound)
                return NotFound(getCostId);

            if (getCostId.IsFailure)
                return BadRequest(getCostId);

            return Ok(getCostId);
        }

        [HttpPost("CreateCost")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCostAsync([FromBody] CostsDto costDto)
        {
            var costCreated = await _cost.CreateCostAsync(costDto);
            if(costCreated.IsFailure)
                return BadRequest(costCreated);

            return Ok(costCreated);
        }

        [HttpPut("UpdateCost/{id}")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] CostsDto costDto, int id)
        {
            var costUpdated = await _cost.UpdateCostAsync(costDto, id);
            if(costUpdated.IsFailure)
                return BadRequest(costUpdated);

            return Ok(costUpdated);
        }

        [HttpDelete("DeleteCost/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCostAsync(int id)
        {
            var costDelete = await _cost.DeleteCostAsync(id);
            if(costDelete.IsFailure)
                return BadRequest(costDelete);

            return Ok(costDelete);
        }

        [HttpGet("GetRelCostPrice")]
        [ProducesResponseType(typeof(IEnumerable<RelPriceCost>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRelCostPriceAsync(DateTime dateIni, DateTime dateEnd)
        {
            var rel = await _cost.GetRelCostPriceAsync(dateIni, dateEnd);
            return Ok(rel);

        }
    }
}
