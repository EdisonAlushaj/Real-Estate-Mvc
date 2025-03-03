using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IShtepiaRepository : IRepository<Shtepia>
    {
        Task<IEnumerable<Shtepia>> GetHousesByType(string type);
    }
}
