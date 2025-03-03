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
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Apartment>> GetAvailableApartments()
        {
            return await _context.Apartments.Where(a => a.Status == "Available").ToListAsync();
        }
    }
}
