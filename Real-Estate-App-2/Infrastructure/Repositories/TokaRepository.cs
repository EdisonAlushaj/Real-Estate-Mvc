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
    public class TokaRepository : GenericRepository<Toka>, ITokaRepository
    {
        public TokaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Toka>> GetTokatByZona(string zona)
        {
            return await _context.Tokat.Where(t => t.Zona == zona).ToListAsync();
        }
    }
}
