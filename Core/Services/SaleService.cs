using Core.Repositories;
using Core.Services.Interfaces;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<Sales> GetByIdSale(int id)
        {
            throw new NotImplementedException();
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
        public async Task<List<Sales>> ReadExcel(Stream stream)
        {
            var sales = new List<Sales>();
            var readExcelExcelToJson = await ReadExcelExcelToJson(stream);
            if(readExcelExcelToJson is null)
                return null;
            else
            {
                sales = await TransformJsontoObj(readExcelExcelToJson);
                var created = await CreateSaleList(sales);
            }
            return sales;
        }
        public async Task<string> CreateSaleList(List<Sales> sale)
        {
           bool result = await _saleRepository.CreateSaleList(sale);
            if(result)
            {
                return "Sucesso"; 
            }
            else
            {
                return "Nenhum registro foi inserido.";
            }
        }
        private async Task<dynamic> ReadExcelExcelToJson(Stream stream)
        {
             ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
             var rows = new List<Dictionary<string, Object>>();
             dynamic result = new ExpandoObject();

            using (var package = new ExcelPackage(stream))
            {
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

            return sales;
        }

    }
}
