using sqlCopyExample.Models.Entities;

namespace sqlCopyExample.Interface.IRepositories;

public interface IProductModelRepository
{
    Task BulkInsertDetail(IEnumerable<ProductModel> productModels);
}
