using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using BusinessLayer.DataTransferObject;
using System.Web;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository iEmployeeRepository;
        public EmployeeService()
        {

        }
        public EmployeeService(IEmployeeRepository _iEmployeeRepository)
        {
            iEmployeeRepository = _iEmployeeRepository;
        }

        /// <summary>
        /// Map Employee Class Objects To EmployeeDto Class Objects
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public EmployeeDto EmployeeMappedToDto(Employee Employee)
        {
            EmployeeDto EmployeeDto = new EmployeeDto();
            EmployeeDto.Name = Employee.Name;
            EmployeeDto.DateOfBirth = Employee.DateOfBirth;
            EmployeeDto.DateOfJoining = Employee.DateOfJoining;
            EmployeeDto.DepartmentId = Employee.DepartmentId;
            EmployeeDto.DepartmentName = Employee.Department.Name;
            EmployeeDto.MobileNumber = Employee.MobileNumber;
            EmployeeDto.Photo = Employee.Photo;
            EmployeeDto.Address = Employee.Address;
            EmployeeDto.EmployeeId = Employee.EmployeeId;

            return EmployeeDto;
        }

        /// <summary>
        /// Map List of Employee Class Objects To List of EmployeeDto Class Objects
        /// </summary>
        /// <param name="Emp"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeDto> EmployeeListMappedToDto(IEnumerable<Employee> EmployeeList)
        {
            var EmployeeDto = new List<EmployeeDto>();
            foreach (Employee Employees in EmployeeList)
            {
                EmployeeDto.Add(new EmployeeDto
                {
                    Name = Employees.Name,
                    DateOfBirth = Employees.DateOfBirth,
                    DateOfJoining = Employees.DateOfJoining,
                    Photo = Employees.Photo,
                    DepartmentId = Employees.DepartmentId,
                    DepartmentName = Employees.Department.Name,
                    Address = Employees.Address,
                    MobileNumber = Employees.MobileNumber,
                    EmployeeId = Employees.EmployeeId
                }
                );
            }

            return EmployeeDto;
        }

        /// <summary>
        /// Get List Of Department Id and Name
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDepartmentList()
        {
            var DepartmentList = iEmployeeRepository.GetDepartmentList();
            return DepartmentList;
        }

        /// <summary>
        /// Add New Employee Details
        /// </summary>
        /// <param name="ImageFile"></param>
        /// <param name="EmployeeDto"></param>
        /// <returns></returns>
        public bool AddEmployees(HttpPostedFileBase ImageFile, EmployeeDto EmployeeDto)
        {
            bool IsAdded = default(bool);
            IsAdded = iEmployeeRepository.AddEmployees(ImageFile, new Employee
            {
                Name = EmployeeDto.Name,
                DateOfBirth = EmployeeDto.DateOfBirth,
                DateOfJoining = EmployeeDto.DateOfJoining,
                Photo = EmployeeDto.Photo,
                DepartmentId = EmployeeDto.DepartmentId,
                MobileNumber = EmployeeDto.MobileNumber,
                Address = EmployeeDto.Address
            });

            return IsAdded;
        }

        /// <summary>
        /// Retrieve List Of Employees 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var EmployeeList = EmployeeListMappedToDto(iEmployeeRepository.GetEmployees());
            return EmployeeList;

        }

        /// <summary>
        /// Search for Employees by id, name, department, joining date
        /// </summary>
        /// <param name="SearchText"></param>
        /// <param name="SearchDate"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeDto> SearchEmployee(string SearchText, DateTime SearchDate)
        {
            var EmployeeList = EmployeeListMappedToDto(iEmployeeRepository.SearchEmployee(SearchText, SearchDate));
            return EmployeeList;

        }

        /// <summary>
        /// Retrieve details of particular authenticated employee
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeDto> GetEmployeeDetail(string Username)
        {
            var EmployeeList = EmployeeListMappedToDto(iEmployeeRepository.GetEmployeeDetail(Username));
            return EmployeeList;

        }
        /// <summary>
        /// Checks for 10 digit mobile number
        /// </summary>
        /// <param name="MobileNo"></param>
        /// <returns></returns>
        public bool IsValidMobileNumber(long MobileNo)
        {
            bool IsValidNo = default(bool);
            IsValidNo = iEmployeeRepository.IsValidMobileNumber(MobileNo);
            return IsValidNo;
        }

        /// <summary>
        /// Check whether mobile number registered and joining date is valid
        /// </summary>
        /// <param name="MobileNumber"></param>
        /// <param name="JoinDate"></param>
        /// <returns></returns>
        public int IsValidDetails(long MobileNumber, DateTime JoinDate)
        {
            int flag = default(int);
            bool IsValidNo = default(bool);
            IsValidNo = IsValidMobileNumber(MobileNumber);

            if (IsValidNo)
            {
                if (JoinDate.Date >= DateTime.Now.Date)
                {
                    flag = 0;
                }
                else
                    flag = -1;
            }
            else
                flag = 1;
            return flag;
        }
    }
}
