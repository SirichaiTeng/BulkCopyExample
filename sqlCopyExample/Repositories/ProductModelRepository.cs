using Microsoft.Data.SqlClient;
using sqlCopyExample.ConnectionFactory;
using sqlCopyExample.Interface.IRepositories;
using sqlCopyExample.Models.Entities;
using sqlCopyExample.Utils;
using System.Reflection;

namespace sqlCopyExample.Repositories;
public class ProductModelRepository : IProductModelRepository
{
    private readonly IDbConnectFactory _dbContext;
    private readonly ILogger<ProductModelRepository> _logger;
    private readonly ISqlBulkCopyFactory _bulkCopyFactory;
    public ProductModelRepository(
        IDbConnectFactory dbConnection,
        ILogger<ProductModelRepository> logger,
        ISqlBulkCopyFactory bulkCopyFactory)
    {
        _dbContext = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _bulkCopyFactory = bulkCopyFactory ?? throw new ArgumentNullException(nameof(bulkCopyFactory));
    }

    public async Task BulkInsertDetail(IEnumerable<ProductModel> productModels)
    {
        if (productModels == null)
        {
            _logger.LogError("ProductModels collection is null");
            throw new ArgumentNullException(nameof(productModels));
        }

        try
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();

            using var bulkCopy = _bulkCopyFactory.Create(connection);
            bulkCopy.DestinationTableName = "[Production].[ProductModel]";
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 600;

            var properties = typeof(ProductModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                bulkCopy.AddColumnMapping(prop.Name, prop.Name);
            }

            var table = MapCollection.MapCollectionToDataTable(productModels);
            await bulkCopy.WriteToServerAsync(table);

            _logger.LogInformation("Successfully inserted {Count} product models", productModels.Count());
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL error during bulk insert to [Production].[ProductModel]");
            throw new Exception("Error during bulk insert: " + ex.Message, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during bulk insert to [Production].[ProductModel]");
            throw new Exception("Unexpected error during bulk insert: " + ex.Message, ex);
        }
    }
}
