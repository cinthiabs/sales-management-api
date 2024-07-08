using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CostController(ICost cost, IMapper mapper) : ControllerBase
    {
        private readonly ICost _cost = cost;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllCosts")]
        [ProducesResponseType(typeof(IEnumerable<CostsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCostsAsync()
        {
            var costs = await _cost.GetCostsAsync();
            var costsDto = _mapper.Map<IEnumerable<CostsDTO>>(costs);
            return Ok(costsDto);
        }

        [HttpGet("GetByIdCost/{id}")]
        [ProducesResponseType(typeof(CostsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdCostAsync(int id)
        {
            if (id == 0)
                return BadRequest("Invalid Cost");

            var cost = await _cost.GetByIdCostAsync(id);
            if (cost is null)
                return NotFound();

            var costDto = _mapper.Map<CostsDTO>(cost);
            return Ok(costDto);
        }

        [HttpPost("CreateCost")]
        [ProducesResponseType(typeof(CostsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCostAsync([FromBody] CostsDTO costDto)
        {
            if (costDto is null)
                return BadRequest("Cost invalid!");

            var cost = _mapper.Map<Costs>(costDto);
            var costCreated = await _cost.CreateCostAsync(cost);
            var costCreatedDto = _mapper.Map<CostsDTO>(costCreated);
            return Ok(costCreatedDto);
        }

        [HttpPut("UpdateCost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] CostsDTO costDto, int id)
        {
            if (costDto is null)
                return BadRequest("Cost invalid!");

            var cost = _mapper.Map<Costs>(costDto);
            cost.Id = id;
            bool costUpdated = await _cost.UpdateCostAsync(cost);
            return costUpdated ? Ok(costUpdated) : BadRequest("Unable to update data!");
        }

        [HttpDelete("DeleteCost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCostAsync(int id)
        {
            if (id == 0 )
                return BadRequest("Cost invalid!");
            bool costDelete = await _cost.DeleteCostAsync(id);
            return costDelete ? Ok(costDelete) : BadRequest("Unable to delete data!");
        }
    }
}
