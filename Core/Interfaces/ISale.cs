using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISale
    {
        Task<string> ReadExcelExcelToJson(Stream stream);
        //internal const string QueryInsertSale = @"";
        //internal const string QueryAlterSale = @"";
        //internal const string QuerySelectSale = @"";
        //internal const string QueryDeleteSale = @"";
        //internal const string QueryGetByIdSale = @"";
    }
}
