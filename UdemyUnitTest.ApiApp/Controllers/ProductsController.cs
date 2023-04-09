using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyUnitTest.Dal.Data;
using UdemyUnitTest.Dal.Models;

namespace UdemyUnitTest.ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepo;

        public ProductsController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepo.GetAllAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto dto)
        {
            if (id != dto.ProductId) return BadRequest();

            var product = await _productRepo.GetByIdAsync(id);

            if (product == null) return NotFound();

            product = dto.Adapt(product);

            var result = await _productRepo.Update(product);

            if (result > 0)
            {
				return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
			}
            else
            {
				return Problem("Cannot be updated");
			}
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDto dto)
        {
			var product = dto.Adapt<Product>();

			var result = await _productRepo.Create(product);

            if (result != null)
            {
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            }
            else
            {
                return Problem("Cannot be created");
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
			var product = await _productRepo.GetByIdAsync(id);

			if (product == null) return NotFound();

			var result = await _productRepo.DeleteByIdAsync(product);

			if (result > 0)
			{
				return NoContent();
			}
			else
			{
				return Problem("Cannot be removed");
			}
		}
    }
}
