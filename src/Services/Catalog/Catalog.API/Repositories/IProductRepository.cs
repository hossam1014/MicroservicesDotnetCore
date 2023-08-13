using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProductById(string id);

        Task<IEnumerable<Product>> GetProductsByName(string productName);

        Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName);

        Task AddProduct(Product product);

        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(string id);

    }
}