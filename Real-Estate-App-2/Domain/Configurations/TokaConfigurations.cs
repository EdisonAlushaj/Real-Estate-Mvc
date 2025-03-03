using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class TokaConfigurations : IEntityTypeConfiguration<Toka>
    {
        public void Configure(EntityTypeBuilder<Toka> builder)
        {
            builder.HasBaseType<Prona>();
            builder.Property(e => e.LandType).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Zona).IsRequired().HasMaxLength(100);
            builder.Property(e => e.TopografiaTokes).IsRequired().HasMaxLength(100);
            builder.Property(e => e.WaterSource).IsRequired();
            builder.ToTable("Tokat");
        }
    }
}
