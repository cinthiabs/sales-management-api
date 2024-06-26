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
        public async Task<Sales> CreateSale(Sales sale)
        {
            var result =  await _saleRepository.CreateSale(sale);
            return result;
        }
        
        public async Task<bool> DeleteSale(int id)
        {
            var record = await _saleRepository.GetByIdSale(id);
            if(record is not null) 
            {
                var rowsAffected = await _saleRepository.DeleteSale(id);
                return rowsAffected;
            }
            return false;
        }

        public async Task<Sales> GetByIdSale(int id)
        {
            var sale = await _saleRepository.GetByIdSale(id);
            return sale;
        }

        public async Task<IEnumerable<Sales>> GetSales()
        {
            var sales = await _saleRepository.GetSales();
            return sales;
        }
        public async Task<bool> UpdateSale(Sales sale)
        {
            var record = await _saleRepository.GetByIdSale(sale.Id);
            if (record is not null)
            {
                var updated = await _saleRepository.UpdateSale(sale);
                return updated;
            }
            return false;     
        }
        public async Task<bool> ReadExcel(Stream stream)
        {
            bool saleBool = false;

            var readExcelExcelToJson = await ReadExcelExcelToJson(stream);
            if (readExcelExcelToJson is not null)
            {
                var sales = await TransformJsontoObjSale(readExcelExcelToJson);
                var created = await CreateSaleList(sales);

                if (created)
                {
                     saleBool = true;
                   // var cost = TransformJsontoObjCost();
                }
                else
                {
                    return false;
                }
            }
            return saleBool;   
        }

        public async Task<bool> CreateSaleList(List<Sales> sale)
        {
            bool result = false;
            

            foreach (var item in sale)
            {
                if( item.Name is not null)
                {
                    var saleExist = await _saleRepository.GetBySaleParameters(item);

                    if (saleExist.Id == 0)
                    {
                        result = await _saleRepository.CreateSaleList(item);
                    }
                    else
                    {
                        result = false;
                    }
                }
                
            }
            return result;
        }
        private async Task<dynamic> ReadExcelExcelToJson(Stream stream)
        {
             ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
             var rows = new List<Dictionary<string, Object>>();
             _ = new ExpandoObject();

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
            return await Task.FromResult((dynamic)rows);

        }
        private async Task<List<Sales>> TransformJsontoObjSale(dynamic obj)
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

        public async Task<IEnumerable<RelQuantity>> GetRelQuantity(DateTime dateIni, DateTime dateEnd)
        {
            var relQuantity = await _saleRepository.GetRelQuantity(dateIni, dateEnd);
            return relQuantity;
        }
    }
}
