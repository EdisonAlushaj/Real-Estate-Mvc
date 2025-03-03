using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ApartmentCreateDto
    {
        public string Emri { get; set; }
        public string Adresa { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int floor { get; set; }
        public int nrDhomave { get; set; }
        public bool kaAnshensor { get; set; }
        public IFormFile Photo { get; set; }
    }
}
