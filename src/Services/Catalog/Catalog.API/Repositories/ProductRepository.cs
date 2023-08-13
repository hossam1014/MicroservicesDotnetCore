using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {

            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            var result = await _context.Products.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, productName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}