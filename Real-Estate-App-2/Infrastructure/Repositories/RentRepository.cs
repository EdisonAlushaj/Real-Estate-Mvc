using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RentRepository : GenericRepository<Rent>, IRentRepository
    {
        public RentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rent>> GetRentsByUserIdAsync(string userId)
        {
            return await _context.Rents
                .Include(r => r.Users)
                .Include(r => r.Pronat)
                .Where(r => r.UserID == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rent>> GetAllRentsWithIncludesAsync()
        {
            return await _context.Rents
                 .Include(r => r.Users)
                 .Include(r => r.Pronat)
                 .ToListAsync();
        }
    }
}
