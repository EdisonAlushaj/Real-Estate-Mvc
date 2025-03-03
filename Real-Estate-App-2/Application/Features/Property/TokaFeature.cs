using Application.DTO;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Property
{
    public class TokaFeature
    {
        private readonly ITokaRepository _tokaRepository; // Inject the repository
        private readonly IAppDbContext _context;

        public TokaFeature(IAppDbContext context, ITokaRepository tokaRepository)
        {
            _context = context;
            _tokaRepository = tokaRepository;
        }

        public async Task<Toka> CreateTokaAsync(TokaCreateDto tokaDto)
        {
            string photoPath = null;

            if (tokaDto.Photo != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Toka-Img");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(tokaDto.Photo.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await tokaDto.Photo.CopyToAsync(fileStream);
                }

                photoPath = Path.Combine("Images", "Toka-Img", fileName);
            }

            var toka = new Toka
            {
                Emri = tokaDto.Emri,
                Adresa = tokaDto.Adresa,
                Price = tokaDto.Price,
                Description = tokaDto.Description,
                Type = tokaDto.Type,
                Status = "Available",
                Photo = photoPath,
                LandType = tokaDto.LandType,
                Zona = tokaDto.Zona,
                TopografiaTokes = tokaDto.TopografiaTokes,
                WaterSource = tokaDto.WaterSource
            };

            //_context.Tokat.Add(toka);
            //await _context.SaveChangesAsync();
            await _tokaRepository.AddAsync(toka);

            return toka;
        }
    }
}
