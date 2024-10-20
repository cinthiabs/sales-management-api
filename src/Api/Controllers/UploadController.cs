﻿using Application.Interfaces;
using Domain.Enums;
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
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var imported = await _upload.ReadExcelAsync(stream);
            
            if (imported.Code == Status.UnableToImportFile)
                return BadRequest(imported);

            return Ok(imported);
        }
    }
}
