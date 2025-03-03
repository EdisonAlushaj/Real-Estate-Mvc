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
    public class SellConfigurations : IEntityTypeConfiguration<Sell>
    {
        public void Configure(EntityTypeBuilder<Sell> builder)
        {
            builder.HasKey(c => c.SellID);
            builder.Property(e => e.SaleDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.SalePrice).IsRequired();
            builder.Property(e => e.Commision).IsRequired();
            builder.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(100);

            builder.HasOne(m => m.Users)
                   .WithMany()
                   .HasForeignKey(m => m.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Pronat)
                   .WithMany()
                   .HasForeignKey(m => m.PronaID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}