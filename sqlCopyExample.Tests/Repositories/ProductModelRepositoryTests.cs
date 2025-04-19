using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Moq;
using sqlCopyExample.ConnectionFactory;
using sqlCopyExample.Interface;
using sqlCopyExample.Models.Entities;
using sqlCopyExample.Repositories;
using System.Data;

public class ProductModelRepositoryTests
{
    private readonly Mock<IDbConnectFactory> _mockDbFactory;
    private readonly Mock<ILogger<ProductModelRepository>> _mockLogger;
    private readonly Mock<ISqlBulkCopyWrapper> _mockBulkCopyWrapper;
    private readonly Mock<ISqlBulkCopyFactory> _mockBulkCopyFactory;
    private readonly Mock<IDbConnection> _mockDbConnection;

    private readonly ProductModelRepository _repository;

    public ProductModelRepositoryTests()
    {
        _mockDbFactory = new Mock<IDbConnectFactory>();
        _mockLogger = new Mock<ILogger<ProductModelRepository>>();
        _mockBulkCopyWrapper = new Mock<ISqlBulkCopyWrapper>();
        _mockBulkCopyFactory = new Mock<ISqlBulkCopyFactory>();
        _mockDbConnection = new Mock<IDbConnection>();

        _mockDbFactory.Setup(f => f.CreateConnection()).Returns(_mockDbConnection.Object);
        _mockDbConnection.Setup(c => c.Open());
        _mockBulkCopyFactory.Setup(f => f.Create(It.IsAny<IDbConnection>())).Returns(_mockBulkCopyWrapper.Object);

        _repository = new ProductModelRepository(
            _mockDbFactory.Object,
            _mockLogger.Object,
            _mockBulkCopyFactory.Object
        );
    }

    [Fact]
    public async Task BulkInsertDetail_ValidData_CallsWriteToServerAndLogs()
    {
        // Arrange
        var data = new List<ProductModel>
        {
            new ProductModel
            {
                ProductModelID = 1,
                Name = "Test",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            }
        };

        // Act
        await _repository.BulkInsertDetail(data);

        // Assert
        _mockBulkCopyWrapper.Verify(b => b.WriteToServerAsync(It.IsAny<DataTable>()), Times.Once);

        _mockLogger.Verify(l => l.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Successfully inserted")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}


