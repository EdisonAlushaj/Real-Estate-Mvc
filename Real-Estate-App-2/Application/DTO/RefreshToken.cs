using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RefreshToken
    {
        public required string Token { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expired { get; set; }
    }
}
