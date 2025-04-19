using CRUDProduct.Domain.Entities;
using Microsoft.Data.SqlClient;
using sqlCopyExample.ConnectionFactory;
using sqlCopyExample.Interface.IRepositories;
using sqlCopyExample.Utils;
using System.Reflection;

namespace sqlCopyExample.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDapperWarpper _dapper;
    private readonly IDbConnectFactory _dbContext;
    public ProductRepository(IDapperWarpper dapperWarpper, IDbConnectFactory dbConnection)
    {
        _dapper = dapperWarpper;
        _dbContext = dbConnection;
    }

    public async Task BulkInsertDetail(IEnumerable<Product> products)
    {
        try
        {
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                using (var bulkCopy = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.TableLock, null))
                {
                    bulkCopy.DestinationTableName = "[Production].[Product]";
                    // กำหนด ColumnMappings
                    var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var prop in properties)
                    {
                        bulkCopy.ColumnMappings.Add(prop.Name, prop.Name);
                    }

                    var table = MapCollection.MapCollectionToDataTable(products);
                    bulkCopy.BatchSize = 5000;
                    bulkCopy.BulkCopyTimeout = 600;

                    await bulkCopy.WriteToServerAsync(table);
                }
            }
        }
        catch (SqlException ex)
        {
            // บันทึกข้อผิดพลาดหรือแจ้งเตือน
            throw new Exception("Error during bulk insert: " + ex.Message, ex);
        }
    }
}
