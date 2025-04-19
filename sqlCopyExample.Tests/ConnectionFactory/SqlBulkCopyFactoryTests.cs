using Microsoft.Data.SqlClient;
using Moq;
using sqlCopyExample.ConnectionFactory;
using sqlCopyExample.Interface;
using System.Data;

namespace sqlCopyExample.Tests.ConnectionFactory;
public class SqlBulkCopyFactoryTests
{
    [Fact]
    public void Create_WithSqlConnection_ReturnsWrapper()
    {
        var factory = new SqlBulkCopyFactory();
        var conn = new SqlConnection("Server=localhost;Database=tempdb;Integrated Security=True;");

        var wrapper = factory.Create(conn);

        Assert.NotNull(wrapper);
        Assert.IsAssignableFrom<ISqlBulkCopyWrapper>(wrapper);
    }

    [Fact]
    public void Create_WithInvalidConnection_ThrowsException()
    {
        var factory = new SqlBulkCopyFactory();
        var mockConn = new Mock<IDbConnection>().Object;

        Assert.Throws<InvalidCastException>(() => factory.Create(mockConn));
    }
}
