using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAppDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Prona> Pronas { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Shtepia> Shtepiat { get; set; }
        public DbSet<Toka> Tokat { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Kontrata> Kontrata { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
