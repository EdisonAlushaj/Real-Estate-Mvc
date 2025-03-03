using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Toka : Prona
    {
        public string LandType { get; set; }
        public string Zona { get; set; }
        public string TopografiaTokes { get; set; }
        public bool WaterSource { get; set; }
    }
}
