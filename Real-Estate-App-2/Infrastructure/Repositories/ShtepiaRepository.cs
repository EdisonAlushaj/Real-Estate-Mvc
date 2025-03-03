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
    public class ShtepiaRepository : GenericRepository<Shtepia>, IShtepiaRepository
    {
        public ShtepiaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Shtepia>> GetHousesByType(string type)
        {
            return await _context.Shtepiat.Where(s => s.Type == type).ToListAsync();
        }
    }
}
