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
    public class PronaRepository : GenericRepository<Prona>, IPronaRepository
    {
        public PronaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Prona>> GetByCategoryAsync(string category)
        {
            switch (category.ToLower())
            {
                case "house":
                    return await _context.Pronas.OfType<Shtepia>().ToListAsync();
                case "land":
                    return await _context.Pronas.OfType<Toka>().ToListAsync();
                case "apartment":
                    return await _context.Pronas.OfType<Apartment>().ToListAsync();
                default:
                    throw new ArgumentException("Invalid category. Valid categories are 'Shtepia', 'Toka', or 'Apartment'.");
            }
        }

        public async Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(string? location, string? category, double? maxPrice, string? propertyType)
        {
            var query = _context.Pronas.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(p => p.Adresa.Contains(location));
            }

            if (!string.IsNullOrEmpty(category))
            {
                switch (category.ToLower())
                {
                    case "house":
                        query = query.OfType<Shtepia>();
                        break;
                    case "land":
                        query = query.OfType<Toka>();
                        break;
                    case "apartment":
                        query = query.OfType<Apartment>();
                        break;
                    default:
                        throw new ArgumentException("Invalid category. Valid categories are 'Shtepia', 'Toka', or 'Apartment'.");
                }
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(propertyType))
            {
                query = query.Where(p => p.Type.Equals(propertyType));
            }

            return await query.ToListAsync();
        }

        public async Task<Prona?> GetPropertyDetailsAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid property ID.");
            }

            return await _context.Pronas.FirstOrDefaultAsync(p => p.PronaID == id);
        }
    }
}
