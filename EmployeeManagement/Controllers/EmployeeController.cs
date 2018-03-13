using BusinessLayer.DataTransferObject;
using BusinessLayer.Services;
using EmployeeManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService iEmployeeService;

        public EmployeeController()
        {

        }


        public EmployeeController(IEmployeeService _iEmployeeService)
        {
            iEmployeeService = _iEmployeeService;
        }

        /// <summary>
        /// Converts List Of Employee Data Transfer Objects to EmployeeViewModel Class Objects
        /// </summary>
        /// <param name="EmployeeDto"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeViewModel> DtoListMappedToView(IEnumerable<EmployeeDto> EmployeeDto)
        {
            var EmployeeView = new List<EmployeeViewModel>();
            foreach (EmployeeDto Employees in EmployeeDto)
            {
                EmployeeView.Add(new EmployeeViewModel
                {
                    Name = Employees.Name,
                    DateOfBirth = Employees.DateOfBirth,
                    DateOfJoining = Employees.DateOfJoining,
                    Photo = Employees.Photo,
                    DepartmentId = Employees.DepartmentId,
                    DepartmentName = Employees.DepartmentName,
                    Address = Employees.Address,
                    MobileNumber = Employees.MobileNumber,
                    EmployeeId = Employees.EmployeeId
                }
                );
            }

            return EmployeeView;
        }

        /// <summary>
        /// Render Employee Profile
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsAdmin"]))
            {
                Dictionary<int, string> PostImages = new Dictionary<int, string>();
                string username = string.Empty;

                username = (string)Session["username"];
                var EmployeeList = DtoListMappedToView(iEmployeeService.GetEmployeeDetail(username));
                ViewBag.Base64String = EmployeeList;

                foreach (var emp in EmployeeList)
                {
                    PostImages.Add(emp.EmployeeId, Convert.ToBase64String(emp.Photo));

                }
                ViewBag.Images = PostImages;
                return View(EmployeeList);
            }
            else
                return RedirectToAction("Index", "Admin");

        }
    }
}