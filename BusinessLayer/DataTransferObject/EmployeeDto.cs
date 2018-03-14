using System;

namespace BusinessLayer.DataTransferObject
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public byte[] Photo { get; set; }
        public string Address { get; set; }
        public long MobileNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Age { get; set; }
        public bool IsLocked { get; set; }
    }
}
