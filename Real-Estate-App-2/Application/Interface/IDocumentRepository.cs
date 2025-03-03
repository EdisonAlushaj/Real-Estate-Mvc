using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IDocumentRepository : IRepository<Documents>
    {
        Task<IEnumerable<Documents>> GetAllDocumentsWithIncludesAsync();
        Task<Documents> GetDocumentByIdWithIncludesAsync(int id);
    }
}
