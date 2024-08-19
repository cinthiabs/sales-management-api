using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class UploadController(IUpload upload) : ControllerBase
    {
        private readonly IUpload _upload = upload;

        [HttpPost("UploadExcel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadExcelAsync(IFormFile file)
        {
            if (file is null)
                return BadRequest("File invalid!");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var imported = await _upload.ReadExcelAsync(stream);
            if (imported.IsFailure)
                return NotFound(imported);

            return Ok(imported);
        }
    }
}
