using Application.Interface;
using Domain.Configurations;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Prona> Pronas { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Shtepia> Shtepiat { get; set; }
        public DbSet<Toka> Tokat { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Kontrata> Kontrata { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PronaConfigurations());

            builder.ApplyConfiguration(new ApartmentsConfigurations());

            builder.ApplyConfiguration(new TokaConfigurations());

            builder.ApplyConfiguration(new ShtepiaConfigurations());

            builder.ApplyConfiguration(new SellConfigurations());

            builder.ApplyConfiguration(new RentConfiguration());

            builder.ApplyConfiguration(new DocumentsConfigurations());

            builder.ApplyConfiguration(new kontrataConfigurations());

            builder.ApplyConfiguration(new ContactRequestConfiguration());
        }
    }
}
