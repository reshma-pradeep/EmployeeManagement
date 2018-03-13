using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataAccessLayer.Repository
{
    public interface IEmployeeRepository
    {
        List<SelectListItem> GetDepartmentList();
        bool AddEmployees(HttpPostedFileBase ImageFile, Employee Employee);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeeDetail(string Username);
        IEnumerable<Employee> SearchEmployee(string SearchText, DateTime SearchDate);
        bool IsValidMobileNumber(long MobileNo);
    }
}
