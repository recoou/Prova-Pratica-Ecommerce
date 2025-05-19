using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class EcommerceDbContext: DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options): base(options) { 
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
