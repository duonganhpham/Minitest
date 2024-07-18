
using Microsoft.EntityFrameworkCore;
using Test.Entities;
using Test.Data;
using Test.Business.Model;


namespace Test.Business.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(ProductRequestModel product)
        {
            var newProduct = new Product
            {
                Id = new Guid(),
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            await _context.Products.AddAsync(newProduct);
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return product;
        }

        public async Task DeleteProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user by Id", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}