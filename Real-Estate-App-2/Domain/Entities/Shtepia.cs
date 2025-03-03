using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Shtepia : Prona
    {
        public double size { get; set; }
        public int nrFloors { get; set; }
        public bool kaGarazhd { get; set; }
        public bool kaPool { get; set; }
    }
}
