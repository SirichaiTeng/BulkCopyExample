using Microsoft.AspNetCore.Mvc;
using sqlCopyExample.Interface.IService;

namespace sqlCopyExample.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpPost("createProduct")]
    public async Task<IActionResult> CreateProducts([FromQuery] int total)
    {
        if (total <= 0)
        {
            _logger.LogWarning("Invalid total value: {Total}", total);
            return BadRequest(new { Message = "Total must be greater than zero." });
        }

        if (total > 100000) // จำกัดจำนวนสูงสุดเพื่อป้องกันการใช้งานหน่วยความจำมากเกินไป
        {
            _logger.LogWarning("Total value too large: {Total}", total);
            return BadRequest(new { Message = "Total cannot exceed 100,000." });
        }

        try
        {
            var result = await _productService.CreateProducts(total);
            if (result)
            {
                _logger.LogInformation("Successfully created {Total} products", total);
                return Ok(new { Message = "Products created successfully", TotalCreated = total });
            }

            _logger.LogWarning("Failed to create {Total} products", total);
            return StatusCode(500, new { Message = "Failed to create products." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating {Total} products", total);
            return StatusCode(500, new { Message = "An error occurred while creating products." });
        }
    }

    [HttpPost("createProductModel")]
    public async Task<IActionResult> CreateProductModels([FromQuery] int total)
    {
        if (total <= 0)
        {
            _logger.LogWarning("Invalid total value: {Total}", total);
            return BadRequest(new { Message = "Total must be greater than zero." });
        }

        if (total > 100000)
        {
            _logger.LogWarning("Total value too large: {Total}", total);
            return BadRequest(new { Message = "Total cannot exceed 100,000." });
        }

        try
        {
            var result = await _productService.CreateProductModels(total);
            if (result)
            {
                _logger.LogInformation("Successfully created {Total} product models", total);
                return Ok(new { Message = "Product models created successfully", TotalCreated = total });
            }

            _logger.LogWarning("Failed to create {Total} product models", total);
            return StatusCode(500, new { Message = "Failed to create product models." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating {Total} product models", total);
            return StatusCode(500, new { Message = "An error occurred while creating product models." });
        }
    }
}
