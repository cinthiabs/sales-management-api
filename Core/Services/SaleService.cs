using Core.Repositories;
using Core.Services.Interfaces;
using Entities.Entities;
using OfficeOpenXml;
using System.Dynamic;

namespace Core.Services
{
    public class SaleService : ISale
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public Task<Sales> CreateSale(Sales sale)
        {
            throw new NotImplementedException();
        }
        

        public Task<Sales> DeleteSale(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Sales>> GetByIdSale(int id)
        {
            var sale = await _saleRepository.GetByIdSale(id);
            return new Response<Sales>
            {
                Status = 200,
                Message = "Success",
                Dados = sale
            };
        }

        public async Task<IEnumerable<Sales>> GetSales()
        {
            var sales = await _saleRepository.GetSales();

            return sales;
        }
        public Task<Sales> UpdateSale(Sales sale)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<List<Sales>>> ReadExcel(Stream stream)
        {
            var sales = new List<Sales>();

            var readExcelExcelToJson = await ReadExcelExcelToJson(stream);
            if (readExcelExcelToJson == null)
            {
                return new Response<List<Sales>>
                {
                    Status = 400,
                    Message = "Sale invalid!",
                    Dados = null 
                };
            }

            sales = await TransformJsontoObj(readExcelExcelToJson);
            var created = await CreateSaleList(sales);

            if (created)
            {
                return new Response<List<Sales>>
                {
                    Status = 200,
                    Message = "Success",
                    Dados = sales
                };
            }
            else
            {
                return new Response<List<Sales>>
                {
                    Status = 400,
                    Message = "Failed to register sales",
                    Dados = null 
                };
            }
        }

        public async Task<bool> CreateSaleList(List<Sales> sale)
        {
            bool result = false;

            foreach (var item in sale)
            {
                var validateInData = _saleRepository.GetBySaleParameters(item);

                if (validateInData == null)
                {
                    result = await _saleRepository.CreateSaleList(item);
                }
            }
            return result;
        }
        private Task<dynamic> ReadExcelExcelToJson(Stream stream)
        {
             ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
             var rows = new List<Dictionary<string, Object>>();
             dynamic result = new ExpandoObject();

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            int totalColumns = worksheet.Dimension.Columns;
            int totalRows = worksheet.Dimension.Rows;

            var columnNames = new List<string>();
            for (int col = 1; col <= totalColumns; col++)
            {
                columnNames.Add(worksheet.Cells[1, col].Text);
            }

            for (int row = 2; row <= totalRows; row++)
            {
                var rowData = new Dictionary<string, object>();
                for (int col = 1; col <= totalColumns; col++)
                {
                    string columnName = columnNames[col - 1];
                    object cellValue = worksheet.Cells[row, col].Text;
                    rowData[columnName] = cellValue;
                }
                rows.Add(rowData);
            }
            return result = rows;

        }
        private async Task<List<Sales>> TransformJsontoObj(dynamic obj)
        {
            var sales = new List<Sales>();

            foreach (var col in obj)
            {
                var sale = new Sales();

                foreach (var row in col)
                {
                    var key = row.Key;
                    var value = row.Value.ToString();

                    if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                    {
                        continue;
                    }

                    if (key == "DATAVENDA")
                    {
                        sale.DateSale = Convert.ToDateTime(value);
                    }
                    else if (key == "PRODUTO")
                    {
                        sale.Name = value;
                    }
                    else if (key == "CLIENTE")
                    {
                        sale.Details = value;
                    }
                    else if (key == "QUANT")
                    {
                        sale.Quantity = Convert.ToInt32(value);
                    }
                    else if (key == "VALORVENDA")
                    {
                        sale.Price = Convert.ToDecimal(value.Trim('R', '$', ','));
                    }
                }

                sales.Add(sale);
            }

            return await Task.FromResult(sales);
        }

    }
}
