using Dapper;
using System.Data;

namespace sqlCopyExample.ConnectionFactory;

public interface IDapperWarpper
{
    Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? patam = null);
    Task<T?> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object? patam = null);
    Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object? patam = null);
    Task<int> ExecuteAsync<T>(IDbConnection connection, string sql, object? patam = null);
    Task<bool> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object? patam = null);
}

public class DapperWarpper : IDapperWarpper
{
    public async Task<int> ExecuteAsync<T>(IDbConnection connection, string sql, object? patam = null)
    {
        return await connection.ExecuteAsync(sql, patam);
    }

    public async Task<bool> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object? patam = null)
    {
        return await connection.ExecuteScalarAsync<int>(sql, patam) > 0;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? patam = null)
    {
        return await connection.QueryAsync<T>(sql, patam);
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object? patam = null)
    {
        return await connection.QueryFirstOrDefaultAsync<T>(sql, patam);
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object? patam = null)
    {
        return await connection.QuerySingleOrDefaultAsync<T>(sql, patam);
    }
}
