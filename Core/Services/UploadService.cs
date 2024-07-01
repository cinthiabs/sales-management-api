using Core.Repositories;
using Core.Services.Interfaces;
using Entities.Entities;
using OfficeOpenXml;
using System.Dynamic;

namespace Core.Services
{
    public class UploadService : IUpload
    {
        private readonly ISale _sale;
        private readonly ICost _cost;

        public UploadService(ISale sale, ICost cost)
        {
            _sale = sale;
            _cost = cost;
        }

        public async Task<bool> ReadExcel(Stream stream)
        {
            bool saleBool = false;

            var file = await ReadExcelExcelToJson(stream);
          

            if (file is not null)
            {
                var sales = await TransformJsontoObjSale(file);
                var created = await _sale.CreateSaleList(sales);


                var costs = await TransformJsontoObjCost(file);
                var cost = await _cost.CreateCostList(costs);
                if (cost)
                    return true;
            }
            return saleBool;
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

        private async Task<List<Costs>> TransformJsontoObjCost(dynamic obj)
        {
            var costs = new List<Costs>();

            foreach (var col in obj)
            {
                var cost = new Costs();

                foreach (var row in col)
                {
                    var key = row.Key;
                    var value = row.Value.ToString();

                    if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                    {
                        continue;
                    }

                    if (key == "DATACOMPRA")
                    {
                        cost.DateCost = Convert.ToDateTime(value);
                    }
                    else if (key == "UNI")
                    {
                        cost.Quantity = value;
                    }
                    else if (key == "COMPRA")
                    {
                        cost.Name = value;
                    }
                    else if (key == "PRECOUNIT")
                    {
                        cost.UnitPrice = Convert.ToDecimal(value.Trim('R', '$', ','));
                    }
                    else if (key == "TOTALCUSTO")
                    {
                        cost.TotalPrice = Convert.ToDecimal(value.Trim('R', '$', ','));
                    }
                }

                costs.Add(cost);
            }

            return await Task.FromResult(costs);
        }
    }
}
