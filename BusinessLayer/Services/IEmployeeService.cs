using BusinessLayer.DataTransferObject;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace BusinessLayer.Services
{
    public interface IEmployeeService
    {
        List<SelectListItem> GetDepartmentList();
        bool AddEmployees(HttpPostedFileBase ImageFile, EmployeeDto EmployeeDto);
        IEnumerable<EmployeeDto> GetEmployees();
        IEnumerable<EmployeeDto> GetEmployeeDetail(string Username);
        IEnumerable<EmployeeDto> SearchEmployee(string SearchText, DateTime SearchDate);
        bool IsValidMobileNumber(long MobileNo);
        
    }
}
