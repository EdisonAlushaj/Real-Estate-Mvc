using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Documents
    {
        public int DocumentId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedData { get; set; }
        public DateTime ExpiorationDate { get; set; }

        public int PronaID { get; set; }
        public Prona Pronat { get; set; }
    }
}
