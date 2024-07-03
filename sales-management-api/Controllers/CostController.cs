using Core.Services.Interfaces;
using Entities.Entities;
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
        public async Task<IActionResult> GetAllCosts()
        {
            var costCreated = await _cost.GetCosts();
            return Ok(costCreated);
        }

        [HttpGet("GetByIdCost")]
        [ProducesResponseType(typeof(Costs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdCost(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Cost");
            }
            var costCreated = await _cost.GetByIdCost(id);
            if(costCreated is null)
            {
                return NotFound();
            }
            return Ok(costCreated);
        }

        [HttpPost("CreateCost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCost([FromBody] Costs cost)
        {
            if (cost is null)
                return BadRequest("Cost invalid!");

            var costCreated = await _cost.CreateCost(cost);
            return Ok(costCreated);
        }

        [HttpPut("UpdateCost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCost([FromBody] Costs cost)
        {
            if (cost is null)
                return BadRequest("Cost invalid!");

            bool costUpdate = await _cost.UpdateCost(cost);
            return costUpdate ? Ok(costUpdate) : BadRequest("Unable to update data!");

        }
        [HttpDelete("DeleteCost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCost(int id)
        {
            if (id == 0)
                return BadRequest("Cost invalid!");
            bool costDelete = await _cost.DeleteCost(id);
            return costDelete ? Ok(costDelete) : BadRequest("Unable to delete data!");
        }
    }
}
