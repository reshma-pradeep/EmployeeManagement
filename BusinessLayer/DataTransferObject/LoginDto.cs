using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObject
{
    public class LoginDto
    {
        public string Username { get; set; }
        public long Password { get; set; }
    }
}
