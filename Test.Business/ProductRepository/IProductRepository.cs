using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Model;
using Test.Entities;

namespace Test.Business.ProductRepository
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(Guid id);
        Task <Product> AddProduct(ProductRequestModel product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Guid id);
        Task SaveChangesAsync();
    }
}
