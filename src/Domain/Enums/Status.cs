using System.ComponentModel;

namespace Domain.Enums
{
    public enum Status
    {
        [Description("No data found!")]
        noDatafound = 0,

        [Description("Invalid client code!")]
        InvalidClient = 1,

        [Description("Invalid product code!")]
        InvalidProduct = 2,

        [Description("Invalid sale code!")]
        InvalidSale = 3,

        [Description("Invalid cost code!")]
        InvalidCost = 4,

        [Description("Product already exists!")]
        ConflitProduct = 5,

        [Description("Sale already exists!")]
        ConflitSale = 6,

        [Description("Data updated successfully!")]
        UpdatedSuccess = 7,

        [Description("Data deleted successfully!")]
        DeletedSuccess = 8,
        
        [Description("Failure to update data!")]
        UpdateFailure = 9,

        [Description("Failure to delete data!")]
        DeleteFailure = 10,

        [Description("Failure to insert data!")]
        InsertFailure = 10,

        [Description("Data is empty!")]
        Empty = 11,

        [Description("Internal error!")]
        InternalError = 500
    }
}
