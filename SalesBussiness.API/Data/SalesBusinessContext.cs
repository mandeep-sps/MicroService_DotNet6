using Microsoft.EntityFrameworkCore;
using SalesBussiness.API.Data.Entities;

namespace SalesBussiness.API.Data
{
    public class SalesBusinessContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SalesBusinessContext(DbContextOptions<SalesBusinessContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public DbSet<Orders> Orders { get; set; }

        public DbSet<Products> Products { get; set; }
    }
}
