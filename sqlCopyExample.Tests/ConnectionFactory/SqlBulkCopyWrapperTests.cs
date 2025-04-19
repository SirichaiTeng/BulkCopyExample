using Microsoft.Data.SqlClient;
using sqlCopyExample.ConnectionFactory;

namespace sqlCopyExample.Tests.ConnectionFactory;
public class SqlBulkCopyWrapperTests
{
    [Fact]
    public void Properties_CanBeSetCorrectly()
    {
        var conn = new SqlConnection("Server=localhost;Database=tempdb;Integrated Security=True;");
        var wrapper = new SqlBulkCopyWrapper(conn)
        {
            DestinationTableName = "TestTable",
            BatchSize = 1000,
            BulkCopyTimeout = 60
        };

        Assert.Equal("TestTable", wrapper.DestinationTableName);
        Assert.Equal(1000, wrapper.BatchSize);
        Assert.Equal(60, wrapper.BulkCopyTimeout);
    }

    [Fact]
    public void AddColumnMapping_AddsMappingWithoutError()
    {
        var conn = new SqlConnection("Server=localhost;Database=tempdb;Integrated Security=True;");
        var wrapper = new SqlBulkCopyWrapper(conn);

        wrapper.AddColumnMapping("Source", "Destination");

        // ไม่มี assert จริง ๆ เพราะ SqlBulkCopy.ColumnMappings ไม่มี public getter
        // แต่อย่างน้อย assert ว่าไม่ throw ก็โอเค
    }

    [Fact]
    public void Dispose_ThrowsNotImplementedException()
    {
        var conn = new SqlConnection("Server=localhost;Database=tempdb;Integrated Security=True;");
        var wrapper = new SqlBulkCopyWrapper(conn);

        Assert.Throws<NotImplementedException>(() => wrapper.Dispose());
    }
}
