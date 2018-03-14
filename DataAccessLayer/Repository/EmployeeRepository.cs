using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DataAccessLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Get List of department ids and names
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDepartmentList()
        {
            using (var Context = new Context())
            {
                var DepartmentList = Context.Departments.Select
                                     (x => new SelectListItem
                                     {
                                         Text = x.Name,
                                         Value = x.DepartmentId.ToString()
                                     }).ToList();

                return DepartmentList;
            }


        }

        /// <summary>
        /// Add New Employee Details To Database
        /// </summary>
        /// <param name="ImageFile"></param>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public bool AddEmployees(HttpPostedFileBase ImageFile, Employee Employee)
        {
            using (var Context = new Context())
            {
                StringBuilder sb = new StringBuilder("e");
                Login Login = new Login();
                Employee.Photo = ConvertToBytes(ImageFile);

                //Add List Of Employees
                Context.Employees.Add(Employee);
                Context.SaveChanges();

                var res = (from p in Context.Employees
                           orderby p.EmployeeId descending
                           select p.EmployeeId).Take(1);

                Employee.EmployeeId = Convert.ToInt32(res.FirstOrDefault());

                if (Employee.EmployeeId == 0)
                    Employee.EmployeeCode = sb.Append(0).ToString();
                else
                    Employee.EmployeeCode = sb.Append(Employee.EmployeeId).ToString();

                Context.Entry(Employee).State = EntityState.Modified;
                Context.SaveChanges();

                //Add login details of new employee
                Login.Username = Employee.EmployeeCode;
                var Password = (from p in Context.Employees
                                orderby p.EmployeeId descending
                                select p.MobileNumber).Take(1);
                Login.Password = Convert.ToInt64(Password.FirstOrDefault());
                Login.Role = "Employee";

                Context.Logins.Add(Login);
                Context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Convert Image File to byte array
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public byte[] ConvertToBytes(HttpPostedFileBase Image)
        {
            byte[] ImageBytes = null;

            BinaryReader Reader = new BinaryReader(Image.InputStream);
            ImageBytes = Reader.ReadBytes((int)Image.ContentLength);

            return ImageBytes;

        }

        /// <summary>
        /// Returns list of employees
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployees()
        {
            using (var Context = new Context())
            {
                var Employees = Context.Employees.Include(Employee => Employee.Department);
                return Employees.ToList();
            }
        }

        /// <summary>
        /// Search for an employee by id, name, date of joining, department
        /// </summary>
        /// <param name="SearchText"></param>
        /// <param name="SearchDate"></param>
        /// <returns></returns>
        public IEnumerable<Employee> SearchEmployee(string SearchText, DateTime SearchDate)
        {
            using (var Context = new Context())
            {
                var Employees = Context.Employees.Where(s => s.EmployeeId.ToString() == SearchText || SearchText == null).Include(Employee => Employee.Department);
                if (!Employees.Any())
                    Employees = Context.Employees.Where(s => s.DateOfJoining == SearchDate).Include(Employee => Employee.Department);

                if (!Employees.Any())
                    Employees = Context.Employees.Where(s => s.Name.StartsWith(SearchText) || SearchText == null).Include(Employee => Employee.Department);

                if (!Employees.Any())
                    Employees = Context.Employees.Where(s => s.Department.Name == SearchText || SearchText == null).Include(Employee => Employee.Department);
                return Employees.ToList();
            }
        }

        /// <summary>
        /// Retrieve details of a particular employee
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeDetail(string Username)
        {
            using (var Context = new Context())
            {
                var EmployeeDetail = Context.Employees.Where(s => s.EmployeeCode == Username).Include(Employee => Employee.Department);
                return EmployeeDetail.ToList();
            }
        }

        /// <summary>
        /// Checks whether mobile number is valid
        /// </summary>
        /// <param name="MobileNo"></param>
        /// <returns></returns>

        public bool IsValidMobileNumber(long MobileNo)
        {
            using (var Context = new Context())
            {
                var NumberResult = Context.Employees.Where(s => s.MobileNumber == MobileNo).FirstOrDefault();
                if (NumberResult == null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Unloack password of user by system admin
        /// </summary>
        /// <param name="id"></param>
        public void Unlock(int id)
        {
            using (var Context = new Context())
            {
                var Employee = Context.Employees.Find(id);
                Employee.IsLocked = false;
                Context.SaveChanges();
            }
        }

    }
}
