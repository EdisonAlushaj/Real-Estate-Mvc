using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IKontrataRepository : IRepository<Kontrata>
    {
        Task<Kontrata> GetKontrataByIdWithIncludesAsync(int id);
        Task<IEnumerable<Kontrata>> GetAllKontrataWithIncludesAsync();
    }
}
