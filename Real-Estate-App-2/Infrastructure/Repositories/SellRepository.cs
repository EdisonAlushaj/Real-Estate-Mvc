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
    public class SellRepository : GenericRepository<Sell>, ISellRepository
    {
        public SellRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sell>> GetSellsByUserIdAsync(string userId)
        {
            return await _context.Sells
                .Include(s => s.Users)
                .Include(s => s.Pronat)
                .Where(s => s.UserID == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Sell>> GetAllSellsWithIncludesAsync()
        {
            return await _context.Sells
                .Include(s => s.Users)
                .Include(s => s.Pronat)
                .ToListAsync();
        }
        public async Task<Sell> GetSellByIdWithIncludesAsync(int id)
        {
            return await _context.Sells
                .Include(s => s.Users)
                .Include(s => s.Pronat)
                .FirstOrDefaultAsync(s => s.SellID == id);
        }
    }
}
