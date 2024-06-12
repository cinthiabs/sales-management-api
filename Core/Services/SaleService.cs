using Core.Interfaces;
using Entities.Entities;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SaleService : ISale
    {
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

        public Task<Sales> GetSales()
        {
            throw new NotImplementedException();
        }
        public Task<Sales> UpdateSale(Sales sale)
        {
            throw new NotImplementedException();
        }
        public async Task<string> ReadExcelExcelToJson(Stream stream)
        {
             ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
             var rows = new List<Dictionary<string, Object>>();

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
                 var json = new { value = rows };
                 return JsonConvert.SerializeObject(json);
             }
          
        }

        
    }
}
