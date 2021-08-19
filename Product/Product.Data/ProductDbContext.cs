using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data
{
    public class ProductDbContext: DbContext
    {


        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasIndex(u => u.SerialNumber)
                .IsUnique();
        }

        public DbSet<Product> Products { get; set; }
    }
}
