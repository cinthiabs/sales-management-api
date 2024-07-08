using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly ICost _cost;
        public CostController(ICost cost)
        {
            _cost = cost;
        }
        [HttpGet("GetAllCosts")]
        [ProducesResponseType(typeof(IEnumerable<Costs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCostsAsync()
        {
            var costCreated = await _cost.GetCostsAsync();
            return Ok(costCreated);
        }

        [HttpGet("GetByIdCost")]
        [ProducesResponseType(typeof(Costs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdCostAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Cost");
            }
            var costCreated = await _cost.GetByIdCostAsync(id);
            if(costCreated is null)
            {
                return NotFound();
            }
            return Ok(costCreated);
        }

        [HttpPost("CreateCost")]
        [ProducesResponseType(typeof(Costs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCostAsync([FromBody] Costs cost)
        {
            if (cost is null)
                return BadRequest("Cost invalid!");

            var costCreated = await _cost.CreateCostAsync(cost);
            return Ok(costCreated);
        }

        [HttpPut("UpdateCost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] Costs cost, int id)
        {
            if (cost is null)
                return BadRequest("Cost invalid!");

            bool costUpdate = await _cost.UpdateCostAsync(cost);
            return costUpdate ? Ok(costUpdate) : BadRequest("Unable to update data!");

        }
        [HttpDelete("DeleteCost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCostAsync(int id)
        {
            if (id == 0)
                return BadRequest("Cost invalid!");
            bool costDelete = await _cost.DeleteCostAsync(id);
            return costDelete ? Ok(costDelete) : BadRequest("Unable to delete data!");
        }
    }
}
