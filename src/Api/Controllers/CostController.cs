using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    public class CostController(ICost cost) : ApiController
    {
        private readonly ICost _cost = cost;

        [HttpGet("GetAllCosts")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCostsAsync()
        {
            var getCosts = await _cost.GetCostsAsync();
            return Response(getCosts);
        }

        [HttpGet("GetByIdCost/{id}")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdCostAsync(int id)
        {
            var getCostId = await _cost.GetByIdCostAsync(id);
            return Response(getCostId);
        }

        [HttpPost("CreateCost")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCostAsync([FromBody] CostsDto costDto)
        {
            var costCreated = await _cost.CreateCostAsync(costDto);
            return Response(costCreated);

        }

        [HttpPut("UpdateCost/{id}")]
        [ProducesResponseType(typeof(Response<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] CostsDto costDto, int id)
        {
            var costUpdated = await _cost.UpdateCostAsync(costDto, id);
            return Response(costUpdated);

        }

        [HttpDelete("DeleteCost/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCostAsync(int id)
        {
            var costDelete = await _cost.DeleteCostAsync(id);
            return Response(costDelete);

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
