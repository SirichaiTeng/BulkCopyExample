namespace sqlCopyExample.Interface.IService;

public interface IProductService
{
    Task<bool> CreateProducts(int total);
    Task<bool> CreateProductModels(int total);
}
