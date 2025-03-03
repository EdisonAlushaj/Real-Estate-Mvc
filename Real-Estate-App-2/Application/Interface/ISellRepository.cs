using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ISellRepository : IRepository<Sell>
    {
        Task<IEnumerable<Sell>> GetSellsByUserIdAsync(string userId);
        Task<IEnumerable<Sell>> GetAllSellsWithIncludesAsync();
        Task<Sell> GetSellByIdWithIncludesAsync(int id);
    }
}
