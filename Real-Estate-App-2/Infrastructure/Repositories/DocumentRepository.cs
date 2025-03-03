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
    public class DocumentRepository : GenericRepository<Documents>, IDocumentRepository
    {
        public DocumentRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Documents>> GetAllDocumentsWithIncludesAsync()
        {
            return await _context.Documents
                .Include(d => d.Pronat)
                .ToListAsync();
        }

        public async Task<Documents> GetDocumentByIdWithIncludesAsync(int id)
        {
            return await _context.Documents
                .Include(d => d.Pronat)
                .FirstOrDefaultAsync(d => d.DocumentId == id);
        }
    }
}
