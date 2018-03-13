using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime DateOfJoining { get; set; }

        public byte[] Photo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter proper 10 digit contact number.")]
        public long MobileNumber { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        
        public string DepartmentName { get; set; }
        public DateTime SearchDate { get; set; }
        public SelectList DepartmentList { get; set; }
    }
}