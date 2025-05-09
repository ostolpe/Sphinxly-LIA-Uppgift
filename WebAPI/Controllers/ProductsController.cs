using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Entities;
using WebAPI.Data.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductEntity product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _productRepository.AddAsync(product);
            if (!success)
                return BadRequest("Could not create product.");

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductEntity updatedProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updatedProduct.Id)
                return BadRequest("ID mismatch.");

            var exists = await _productRepository.GetAsync(id);
            if (exists == null)
                return NotFound();

            var success = await _productRepository.UpdateAsync(updatedProduct);
            if (!success)
                return StatusCode(500, "Update failed.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _productRepository.GetAsync(id);
            if (exists == null)
                return NotFound();

            var success = await _productRepository.DeleteAsync(id);
            if (!success)
                return StatusCode(500, "Delete failed.");

            return NoContent();
        }
    }
}
