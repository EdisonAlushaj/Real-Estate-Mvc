using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRentRepository : IRepository<Rent>
    {
        Task<IEnumerable<Rent>> GetRentsByUserIdAsync(string userId);
        Task<IEnumerable<Rent>> GetAllRentsWithIncludesAsync();
    }
}
