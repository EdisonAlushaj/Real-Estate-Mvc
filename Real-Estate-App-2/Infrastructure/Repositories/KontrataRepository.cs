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
    public class KontrataRepository : GenericRepository<Kontrata>, IKontrataRepository
    {
        public KontrataRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Kontrata> GetKontrataByIdWithIncludesAsync(int id)
        {
            return await _context.Kontrata
                .Include(k => k.Users)
                .Include(k => k.Pronat)
                .FirstOrDefaultAsync(k => k.KontrataId == id);
        }
        public async Task<IEnumerable<Kontrata>> GetAllKontrataWithIncludesAsync()
        {
            return await _context.Kontrata
            .Include(k => k.Users)
            .Include(k => k.Pronat)
            .ToListAsync();
        }
    }
}
