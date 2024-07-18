
using Microsoft.EntityFrameworkCore;
using Test.Business.Model;
using Test.Business.ProductRepository;
using Test.Business.UserRepository;
using Test.Entities;


namespace Test.Business.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> GetProductById(Guid id)
        {
            return await _productRepository.GetProductById(id);
        }
        public async Task<Product> AddProduct(ProductRequestModel product)
        {
            var newProduct = await _productRepository.AddProduct(product);
            await _productRepository.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var updateProduct = await _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();
            return updateProduct;
        }
        public async Task DeleteProduct(Guid id)
        {
            await _productRepository.DeleteProduct(id);
            await _productRepository.SaveChangesAsync();
        }
    }
}
