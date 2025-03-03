using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TokaCreateDto
    {
        public string Emri { get; set; }
        public string Adresa { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string LandType { get; set; }
        public string Zona { get; set; }
        public string TopografiaTokes { get; set; }
        public bool WaterSource { get; set; }
        public IFormFile Photo { get; set; }
    }
}
