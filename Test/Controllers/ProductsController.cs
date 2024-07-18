using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Test.Business.Model;
using Test.Business.ProductRepository;
using Test.Business.ProductService;
using Test.Entities;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ProductsController(IProductService productService, IHubContext<NotificationHub> hubContext)
        {
            _productService = productService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequestModel product)
        {
            var connectionId = HttpContext.Request.Headers["X-Connection-Id"]; // Giả sử bạn gửi connectionId qua header

            var addedProduct = await _productService.AddProduct(product);

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", "Product added", addedProduct);
            }

            return Ok(addedProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductRequestModel product)
        {
            var connectionId = HttpContext.Request.Headers["X-Connection-Id"]; 
            var dbProduct = await _productService.GetProductById(id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;

            var updatedProduct = await _productService.UpdateProduct(dbProduct);

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", "Product updated", updatedProduct);
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var connectionId = HttpContext.Request.Headers["X-Connection-Id"]; 

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id);

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", "Product deleted", product);
            }

            return Ok();
        }
    }
}

