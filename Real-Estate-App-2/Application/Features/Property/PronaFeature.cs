using Application.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Property
{
    public class PronaFeature : IPronaFeature
    {
        private readonly IPronaRepository _pronaRepository;

        public PronaFeature(IPronaRepository pronaRepository)
        {
            _pronaRepository = pronaRepository;
        }

        public async Task<IEnumerable<Prona>> GetByCategoryAsync(string category)
        {
            return await _pronaRepository.GetByCategoryAsync(category);
        }

        public async Task<IEnumerable<Prona>> GetAllPropertiesAsync()
        {
            return await _pronaRepository.GetAllAsync(); // Uses generic repository method
        }

        public async Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(string? location, string? category, double? maxPrice, string? propertyType)
        {
            return await _pronaRepository.GetFilteredPropertiesAsync(location, category, maxPrice, propertyType);
        }

        public async Task<Prona?> GetPropertyDetailsAsync(int id)
        {
            return await _pronaRepository.GetPropertyDetailsAsync(id);
        }
    }
}
