using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace sales_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISale _sale;
        public SaleController(ISale sale)
        {
            _sale = sale;   
        }
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file is null && file?.Length == 0)
                return BadRequest("Arquivo inválido");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var resultado = await _sale.ReadExcelExcelToJson(stream);
                Console.WriteLine(resultado);
            }
            return Ok(file);
        }
    }
}
