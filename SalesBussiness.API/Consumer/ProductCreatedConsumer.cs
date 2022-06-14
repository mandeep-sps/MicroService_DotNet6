using MassTransit;
using SalesBussiness.API.Data;
using SalesBussiness.API.Data.Entities;
using Shared.Models.RabbitMqModels;

namespace SalesBussiness.API.Consumer
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly SalesBusinessContext _salesBusinessContext;
        public ProductCreatedConsumer(SalesBusinessContext salesBusinessContext)
        {
            _salesBusinessContext = salesBusinessContext;
        }

        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var newProduct = new Products
            {
                Id = context.Message.Id,
                Name = context.Message.Name
            };
           await _salesBusinessContext.AddAsync(newProduct);
            await _salesBusinessContext.SaveChangesAsync();
        }
    }
}
