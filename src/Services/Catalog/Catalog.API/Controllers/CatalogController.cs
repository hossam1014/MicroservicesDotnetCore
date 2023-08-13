using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{

    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly IProductRepository _repo;

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger, IProductRepository repo)
        {
            _logger = logger;
            _repo = repo;

        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products =  await _repo.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product =  await _repo.GetProductById(id);

            if(product == null) 
            {   
                _logger.LogError($"the product with Id {id} , not found");
                return NotFound();
            }

            return Ok(product);
        }


        // Action is the method Name
        [Route("[Action]/{categoryName}", Name = "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
            var products =  await _repo.GetProductsByCategoryName(categoryName);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _repo.AddProduct(product);

            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repo.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repo.DeleteProduct(id));
        }

    }
}