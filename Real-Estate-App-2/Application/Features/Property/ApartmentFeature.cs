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
    public class ApartmentFeature
    {
        private readonly IApartmentRepository _apartmentRepository; // Inject repository
        private readonly IAppDbContext _context; // Inject AppDbContext

        public ApartmentFeature(IAppDbContext context, IApartmentRepository apartmentRepository)
        {
            _context = context;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Apartment> CreateApartmentAsync(ApartmentCreateDto apartmentDto)
        {
            string photoPath = null;

            if (apartmentDto.Photo != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Apartment-Img");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(apartmentDto.Photo.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await apartmentDto.Photo.CopyToAsync(fileStream);
                }

                photoPath = Path.Combine("Images", "Apartment-Img", fileName);
            }

            var apartment = new Apartment
            {
                Emri = apartmentDto.Emri,
                Adresa = apartmentDto.Adresa,
                Price = apartmentDto.Price,
                Description = apartmentDto.Description,
                Status = "Available",
                Type = apartmentDto.Type,
                Photo = photoPath,
                floor = apartmentDto.floor,
                nrDhomave = apartmentDto.nrDhomave,
                kaAnshensor = apartmentDto.kaAnshensor
            };

            //_context.Apartments.Add(apartment);
            //await _context.SaveChangesAsync();
            await _apartmentRepository.AddAsync(apartment);

            return apartment;
        }
    }
}
