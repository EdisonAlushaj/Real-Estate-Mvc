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
    public class PronaConfigurations : IEntityTypeConfiguration<Prona>
    {
        public void Configure(EntityTypeBuilder<Prona> builder)
        {
            builder.HasKey(c => c.PronaID);
            builder.Property(e => e.Emri).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Adresa).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            builder.Property(e => e.Status).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Type).IsRequired().HasMaxLength(100);
        }
    }
}
