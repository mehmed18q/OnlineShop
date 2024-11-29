using Application.Interfaces;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a Product",
            Description = "Get a Product with Id",
            OperationId = "Product.Get",
            Tags = ["ProductController"])]
        public async Task<IActionResult> Get(int id)
        {
            ProductDto result = await _productService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductDto> result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto model)
        {
            ProductDto result = await _productService.Add(model);
            return Ok(result);
        }
    }
}
