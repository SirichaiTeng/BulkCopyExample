using CRUDProduct.Domain.Entities;
using sqlCopyExample.Interface.IRepositories;
using sqlCopyExample.Interface.IService;
using sqlCopyExample.Models.Entities;

namespace sqlCopyExample.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductModelRepository _productModelRepository;

    public ProductService(IProductRepository productRepository, IProductModelRepository productModelRepository)
    {
        _productRepository = productRepository;
        _productModelRepository = productModelRepository;
    }

    public async Task<bool> CreateProductModels(int total)
    {
        if (total <= 0) return false;

        var productModels = GenerateProductModels(total);
        try
        {
            await _productModelRepository.BulkInsertDetail(productModels);
            return true;
        }
        catch
        {
            return false;
        }
    }
    private IEnumerable<ProductModel> GenerateProductModels(int total)
    {
        var productModels = new List<ProductModel>();
        var random = new Random();

        for (int i = 0; i < total; i++)
        {
            productModels.Add(new ProductModel
            {
                ProductModelID = i + 21510, // หรือใช้ Auto-increment ในฐานข้อมูล
                Name = $"Model_{i + 21510}",
                CatalogDescription =  null,
                Instructions =  null,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            });
        }

        return productModels;
    }
    public async Task<bool> CreateProducts(int total)
    {
        if (total <= 0)
        {
            return false; // หรือ throw Exception ตามความเหมาะสม
        }

        var products = GenerateProducts(total);
        try
        {
            await _productRepository.BulkInsertDetail(products);
            return true;
        }
        catch
        {
            return false; // หรือบันทึกข้อผิดพลาดลง log
        }
    }



    private IEnumerable<Product> GenerateProducts(int total)
    {
        var products = new List<Product>();
        var random = new Random();

        for (int i = 0; i < total; i++)
        {
            var product = new Product
            {
                ProductID = i + 30000, // หรือใช้ Auto-increment ในฐานข้อมูล
                Name = $"Product_{i + 1}",
                ProductNumber = $"PN-{i + 1:D6}",
                MakeFlag = true,
                FinishedGoodsFlag = true,
                Color = GetRandomColor(random),
                SafetyStockLevel = (short)(100 + random.Next(50, 200)),
                ReorderPoint = (short)(75 + random.Next(20, 100)),
                StandardCost = (decimal)(50.0 + random.NextDouble() * 100),
                ListPrice = (decimal)(100.0 + random.NextDouble() * 200),
                Size = random.Next(0, 2) == 0 ? "M" : "L",
                SizeUnitMeasureCode = "CM",
                WeightUnitMeasureCode = "KG",
                Weight = random.Next(0, 2) == 0 ? (decimal?)null : (decimal)(1.0 + random.NextDouble() * 10),
                DaysToManufacture = random.Next(1, 5),
                ProductLine = "R ",
                Class = "M ",
                Style = "U ",
                ProductSubcategoryID = random.Next(1, 10),
                ProductModelID = random.Next(1, 10),
                SellStartDate = DateTime.Now.AddDays(-random.Next(30, 365)),
                SellEndDate = random.Next(0, 2) == 0 ? (DateTime?)null : DateTime.Now.AddDays(random.Next(30, 365)),
                DiscontinuedDate = null,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };
            products.Add(product);
        }

        return products;
    }

    private string GetRandomColor(Random random)
    {
        string[] colors = { "Red", "Blue", "Green", "Black", "White" };
        return colors[random.Next(colors.Length)];
    }
}
