using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace sqlCopyExample.ConnectionFactory;
public interface IDbConnectFactory
{
    IDbConnection CreateConnection();
}
public class DbConnectionFactory : IDbConnectFactory
{
    private readonly IConfiguration _configuration;
    public DbConnectionFactory(IConfiguration configuretion)
    {
        _configuration = configuretion ?? throw new ArgumentException(nameof(configuretion));
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("sqlserver") ?? "";
        return new SqlConnection(connectionString);
    }
}
