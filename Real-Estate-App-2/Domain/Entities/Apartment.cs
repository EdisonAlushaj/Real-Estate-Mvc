using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Apartment : Prona
    {
        public int floor { get; set; }
        public int nrDhomave { get; set; }
        public bool kaAnshensor { get; set; }
    }
}
