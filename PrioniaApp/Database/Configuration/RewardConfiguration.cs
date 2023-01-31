using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Database.Models;

namespace PrioniaApp.Database.Configuration
{

    public class RewardConfiguration : IEntityTypeConfiguration<Reward>
    {
        public void Configure(EntityTypeBuilder<Reward> builder)
        {
            builder
               .ToTable("Rewards");
        }
    }
}
