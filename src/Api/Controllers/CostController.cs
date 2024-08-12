using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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
        [ProducesResponseType(typeof(Response<CostsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCostsAsync()
        {
            var costs = await _cost.GetCostsAsync();
            if(costs.Code == Status.noDatafound)
                return NotFound(costs);
            
            var costsDto = _mapper.Map<IEnumerable<CostsDTO>>(costs);
            return Ok(costsDto);
        }

        [HttpGet("GetByIdCost/{id}")]
        [ProducesResponseType(typeof(Response<CostsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdCostAsync(int id)
        {
            var cost = await _cost.GetByIdCostAsync(id);
            if (cost.IsFailure)
                return NotFound(cost);

            var costDto = _mapper.Map<CostsDTO>(cost.Data.FirstOrDefault());
            return Ok(costDto);
        }

        [HttpPost("CreateCost")]
        [ProducesResponseType(typeof(Response<CostsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCostAsync([FromBody] CostsDTO costDto)
        {
            var cost = _mapper.Map<Costs>(costDto);
            var costCreated = await _cost.CreateCostAsync(cost);
            if(costCreated.IsFailure)
                return BadRequest(costCreated);

            var costCreatedDto = _mapper.Map<CostsDTO>(costCreated.Data);
            return Ok(costCreatedDto);
        }

        [HttpPut("UpdateCost/{id}")]
        [ProducesResponseType(typeof(Response<CostsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] CostsDTO costDto, int id)
        {
            var cost = _mapper.Map<Costs>(costDto);
            cost.Id = id;
            var costUpdated = await _cost.UpdateCostAsync(cost);
            if(costUpdated.IsFailure)
                return BadRequest(costUpdated);

            var costDTO = _mapper.Map<CostsDTO>(costUpdated.Data.FirstOrDefault());
            return Ok(Response<CostsDTO>.Success(costDTO));
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
