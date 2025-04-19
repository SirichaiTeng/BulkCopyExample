using System.Data;

namespace sqlCopyExample.Interface;

public interface ISqlBulkCopyWrapper : IDisposable
{
    string DestinationTableName { get; set; }
    int BatchSize { get; set; }
    int BulkCopyTimeout { get; set; }

    void AddColumnMapping(string sourceColumn, string destinationColumn);
    Task WriteToServerAsync(DataTable table);
}