using Microsoft.Data.SqlClient;
using sqlCopyExample.Interface;
using System.Data;

namespace sqlCopyExample.ConnectionFactory;

public class SqlBulkCopyWrapper : ISqlBulkCopyWrapper
{
    private readonly SqlBulkCopy _bulkCopy;

    public SqlBulkCopyWrapper(SqlConnection connection)
    {
        _bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, null);
    }

    public string DestinationTableName
    {
        get => _bulkCopy.DestinationTableName;
        set => _bulkCopy.DestinationTableName = value;
    }

    public int BatchSize
    {
        get => _bulkCopy.BatchSize;
        set => _bulkCopy.BatchSize = value;
    }

    public int BulkCopyTimeout
    {
        get => _bulkCopy.BulkCopyTimeout;
        set => _bulkCopy.BulkCopyTimeout = value;
    }

    public void AddColumnMapping(string sourceColumn, string destinationColumn)
    {
        _bulkCopy.ColumnMappings.Add(sourceColumn, destinationColumn);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task WriteToServerAsync(DataTable table) => _bulkCopy.WriteToServerAsync(table);


}