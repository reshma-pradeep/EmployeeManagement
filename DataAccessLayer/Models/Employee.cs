using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public byte[] Photo { get; set; }
        public string Address { get; set; }
        public long MobileNumber { get; set; }

        public virtual int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
