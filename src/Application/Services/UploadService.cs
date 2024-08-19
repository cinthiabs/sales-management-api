using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using OfficeOpenXml;
using System.Dynamic;

namespace Application.Services
{
    public class UploadService(ISale sale, ICost cost) : IUpload
    {
        private readonly ISale _sale = sale;
        private readonly ICost _cost = cost;

        public async Task<Response<bool>> ReadExcelAsync(Stream stream)
        {
            try
            {
                var file = await ReadExcelExcelToJsonAsync(stream);

                if (file is null)
                    return Response<bool>.Failure(Status.Empty);

                var sales = await TransformJsontoObjSaleAsync(file);
                var salesCreated = await _sale.CreateSaleListAsync(sales);
                if (!salesCreated)
                    return Response<bool>.Failure(Status.UnableToImportSales);

                var costs = await TransformJsontoObjCostAsync(file);
                var costsCreated = await _cost.CreateCostListAsync(costs);
                if (!costsCreated)
                    return Response<bool>.Failure(Status.UnableToImportCost);

                return Response<bool>.Success(true, Status.ImportedSuccess);
            }
            catch (Exception)
            {
                return Response<bool>.Failure(Status.UnableToImportFile);
            }
        }
        private static async Task<dynamic> ReadExcelExcelToJsonAsync(Stream stream)
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
        private static async Task<List<Sales>> TransformJsontoObjSaleAsync(dynamic obj)
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
                    else if(key == "PAGO")
                    {
                        if (value.Contains("SIM"))
                            sale.Pay = Situation.Pago;
                        else
                            sale.Pay = Situation.Pendente;
                    }
                }

                sales.Add(sale);
            }

            return await Task.FromResult(sales);
        }

        private static async Task<List<Costs>> TransformJsontoObjCostAsync(dynamic obj)
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
