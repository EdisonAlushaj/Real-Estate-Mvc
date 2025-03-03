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
    public class ApartmentsConfigurations : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasBaseType<Prona>();

            builder.Property(e => e.floor).IsRequired();
            builder.Property(e => e.nrDhomave).IsRequired();
            builder.Property(e => e.kaAnshensor).IsRequired();
            builder.ToTable("Apartments");
        }
    }
}
