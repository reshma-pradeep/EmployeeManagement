using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public long Password { get; set; }
        public string Role { get; set; }
    }
}
