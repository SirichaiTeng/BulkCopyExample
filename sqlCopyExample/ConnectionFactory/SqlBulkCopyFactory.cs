using Microsoft.Data.SqlClient;
using sqlCopyExample.Interface;
using System.Data;

namespace sqlCopyExample.ConnectionFactory;

public interface ISqlBulkCopyFactory
{
    ISqlBulkCopyWrapper Create(IDbConnection connection);
}
public class SqlBulkCopyFactory : ISqlBulkCopyFactory
{
    public ISqlBulkCopyWrapper Create(IDbConnection connection)
    {
        if (connection is not SqlConnection sqlConn)
            throw new InvalidCastException("Connection must be SqlConnection.");

        return new SqlBulkCopyWrapper(sqlConn);
    }
}
