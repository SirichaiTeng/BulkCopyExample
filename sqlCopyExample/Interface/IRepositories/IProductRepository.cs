using CRUDProduct.Domain.Entities;

namespace sqlCopyExample.Interface.IRepositories;

public interface IProductRepository
{
    Task BulkInsertDetail(IEnumerable<Product> products);
}
