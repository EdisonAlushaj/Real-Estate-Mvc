using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        Task<IEnumerable<Apartment>> GetAvailableApartments();
    }
}
