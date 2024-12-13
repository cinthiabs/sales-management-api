using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult Response<T>(Response<T> response)
        {
            if (response == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");

            return response.Code switch
            {
                Status.noDatafound => NotFound(response),
                Status.ConflitSale => Conflict(response),
                Status.ConflitProduct => Conflict(response),
                Status.ConflitUser => Conflict(response),
                Status.InvalidPassword => Unauthorized(response),
                _ when response.IsFailure => BadRequest(response),
                _ => Ok(response)
            };
        }
    }
}