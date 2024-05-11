using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Product;
using Shared.Models.Products;
using Shared.Models.Products;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _productService.GetsAsync();
            return Ok(result);
        }
        [HttpGet("GetsByCategory")]
        public async Task<IActionResult> GetsByCategory(int categoryId)
        {
            var result = await _productService.GetsByCategoryAsync(categoryId);
            return Ok(result);
        }

       
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddRequestDto product)
        {
            var category = await _productService.FindByIdAsync(product.CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            await _productService.AddAsync(product);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]Product product)
        {
            await _productService.EditAsync(product);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequestDto model)
        {
            var result = await _productService.SearchAsync(model);
            return Ok(result);
        }
    }
}
