using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUpload _upload;
        public UploadController(IUpload upload)
        {
            _upload = upload;
        }
        [HttpPost("UploadExcel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadExcelAsync(IFormFile file)
        {
            if (file is null && file?.Length == 0)
                return BadRequest("File invalid!");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var resultado = await _upload.ReadExcelAsync(stream);
            return resultado ? Ok(resultado) : Conflict(resultado);
        }
    }
}
