using sqlCopyExample.Models.Entities;
using sqlCopyExample.Utils;

namespace sqlCopyExample.Tests.Utils;
public class DataTableUtilsTests
{
    [Fact]
    public void ConvertToTable_WithValidInput_ReturnsDataTable()
    {
        var list = new List<ProductModel>
            {
                new ProductModel { ProductModelID = 1, Name = "Test", rowguid = Guid.NewGuid(), ModifiedDate = DateTime.UtcNow }
            };

        var table = MapCollection.MapCollectionToDataTable(list);

        Assert.NotNull(table);
        Assert.Single(table.Rows);
        Assert.Equal("Test", table.Rows[0]["Name"]);
    }

    [Fact]
    public void ConvertToTable_WithNullables_HandlesDBNull()
    {
        var list = new List<ProductModel>
    {
        new ProductModel { ProductModelID = 1, Name = null, rowguid = Guid.NewGuid(), ModifiedDate = DateTime.UtcNow }
    };

        var table = MapCollection.MapCollectionToDataTable(list);

        Assert.Equal(DBNull.Value, table.Rows[0]["Name"]);
    }
}
