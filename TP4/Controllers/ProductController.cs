using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using TP4.Interfaces;
using TP4.Models;

namespace TP4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            var product = await _productRepository.GetProductByID(id);
            return  Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {

            await _productRepository.InsertProduct(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Product product)
        {
            if (product != null)
            {
                await  _productRepository.UpdateProduct(product);
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productRepository.DeleteProduct(id);
            return Ok();
        }
    }
}
