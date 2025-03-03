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
    public class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequest>
    {
        public void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);          
            builder.Property(e => e.Message).IsRequired().HasMaxLength(1000);
           
        }
    }
}
