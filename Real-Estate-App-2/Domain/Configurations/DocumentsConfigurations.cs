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
    public class DocumentsConfigurations : IEntityTypeConfiguration<Documents>
    {
        public void Configure(EntityTypeBuilder<Documents> builder)
        {
            builder.HasKey(c => c.DocumentId);
            builder.Property(e => e.Type).IsRequired().HasMaxLength(50);
            builder.Property(e => e.CreatedData).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.ExpiorationDate).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasOne(m => m.Pronat)
                  .WithMany()
                  .HasForeignKey(m => m.PronaID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
