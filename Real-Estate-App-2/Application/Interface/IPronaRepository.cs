﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IPronaRepository : IRepository<Prona>
    {
        Task<IEnumerable<Prona>> GetByCategoryAsync(string category);
        Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(string? location, string? category, double? maxPrice, string? propertyType);
        Task<Prona?> GetPropertyDetailsAsync(int id);
    }
}
