using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Database.Models;

namespace PrioniaApp.Database.Configuration
{

    public class ProductCatagoryConfiguration : IEntityTypeConfiguration<ProductCatagory>
    {
        public void Configure(EntityTypeBuilder<ProductCatagory> builder)
        {
            builder
               .ToTable("ProductCatagoryies");
        }
    }
}
