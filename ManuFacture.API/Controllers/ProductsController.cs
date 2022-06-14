using ManuFacture.API.Data;
using ManuFacture.API.Data.Entities;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.RabbitMqModels;

namespace ManuFacture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ManufactureContext _manufactureContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public ProductsController(ManufactureContext manufactureContext,
            IPublishEndpoint publishEndpoint

        )
        {
            _manufactureContext = manufactureContext;
            _publishEndpoint= publishEndpoint;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Products obj1 = new Products();
            Products obj2 = new Products();
            obj1.Id = 1;
            obj1.Name = "Test";
            obj1.Cost = 100;
            obj1.Description = "Microservices using ASP.NET Core";

            obj2.Id = 2;
            obj2.Name = "Test1223";
            obj2.Cost = 345;
            obj2.Description = "In this article, I am going to explain Microservices using ASP.NET Core Application with Examples. At the end of this article, you will understand the following pointers.";

            List<Products> List = new List<Products>
            {
                obj1,
                obj2

            };
            //var products = await _manufactureContext.Products.AsNoTracking().ToListAsync();
            return Ok(List);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _manufactureContext.Products.FindAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Products newProduct)
        {
           await _manufactureContext.Products.AddAsync(newProduct);
            await _manufactureContext.SaveChangesAsync();

            await _publishEndpoint.Publish<ProductCreated>(new ProductCreated
            {
                Id = newProduct.Id,
                Name = newProduct.Name
            });

            return CreatedAtAction("Get", new { id = newProduct.Id }, newProduct);
        }
    }
}
