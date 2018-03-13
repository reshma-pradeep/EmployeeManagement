using BusinessLayer.DataTransferObject;
using BusinessLayer.Services;
using EmployeeManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IEmployeeService iEmployeeService;
        public AdminController()
        {

        }

        public AdminController(IEmployeeService _iEmployeeService)
        {
            iEmployeeService = _iEmployeeService;
        }

        /// <summary>
        /// Converts Employee Data Transfer Object to Employee View Model Object
        /// </summary>
        /// <param name="EmployeeDto"></param>
        /// <returns>EmployeeViewModel Object</returns>

        public EmployeeViewModel MapToViewModel(EmployeeDto EmployeeDto)
        {
            var EmployeeViewModel = new EmployeeViewModel();
            EmployeeViewModel.Name = EmployeeDto.Name;
            EmployeeViewModel.DateOfBirth = EmployeeDto.DateOfBirth;
            EmployeeViewModel.DateOfJoining = EmployeeDto.DateOfJoining;
            EmployeeViewModel.Photo = EmployeeDto.Photo;
            EmployeeViewModel.DepartmentId = EmployeeDto.DepartmentId;
            EmployeeViewModel.DepartmentName = EmployeeDto.DepartmentName;
            EmployeeViewModel.Address = EmployeeDto.Address;
            EmployeeViewModel.MobileNumber = EmployeeDto.MobileNumber;

            return EmployeeViewModel;
        }

        /// <summary>
        /// Converts List Of Employee Data Transfer Object to Employee View Model Object List
        /// </summary>
        /// <param name="EmployeeeDto"></param>
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

        // GET: Employee
        public ActionResult Index(EmployeeViewModel EmployeeView)
        {
            ModelState.Clear();
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                EmployeeView.DepartmentList = new SelectList(iEmployeeService.GetDepartmentList(), "Text", "Value");

                return View(EmployeeView);
            }
            else
                return RedirectToAction("Index", "Employee");

        }

        /// <summary>
        /// Add New Employee Details
        /// </summary>
        /// <param name="EmployeeView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EmployeeViewModel EmployeeView)
        {

            bool IsAdded = default(bool);
            int flag = default(int);

            if (Convert.ToBoolean(Session["IsAdmin"]))
            {

                EmployeeView.DepartmentList = new SelectList(iEmployeeService.GetDepartmentList(), "Text", "Value");
                flag = IsValidDetails(EmployeeView.MobileNumber, EmployeeView.DateOfJoining);
                if (flag == 0)
                {
                    if (ModelState.IsValid)
                    {
                        HttpPostedFileBase ImageFile = Request.Files["EmployeeImage"];

                        IsAdded = iEmployeeService.AddEmployees(ImageFile,
                        new EmployeeDto
                        {
                            Name = EmployeeView.Name,
                            DateOfBirth = EmployeeView.DateOfBirth,
                            DateOfJoining = EmployeeView.DateOfJoining,
                            Photo = EmployeeView.Photo,
                            DepartmentId = EmployeeView.DepartmentId,
                            Address = EmployeeView.Address,
                            MobileNumber = EmployeeView.MobileNumber,
                            EmployeeId = EmployeeView.EmployeeId
                        }
                        );
                    }
                }

            }
            else
                return RedirectToAction("Index", "Employee");
            if (IsAdded == true)
            {
                TempData["message"] = "Employee details added succssfully";
                return View("Index", EmployeeView);
            }
            else
            {
                if (flag == 1)
                    TempData["message"] = "Sorry number already registered";
                else if (flag == -1)
                    TempData["message"] = "Please select a valid joining date";
                else
                    TempData["message"] = "Please fill in the fields to continue";
                return View("Index", EmployeeView);
            }

        }

        /// <summary>
        /// Display details of all employees
        /// </summary>
        /// <param name="EmployeeView"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(EmployeeViewModel EmployeeView)
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                Dictionary<int, string> PostImages = new Dictionary<int, string>();

                var EmployeeList = DtoListMappedToView(iEmployeeService.GetEmployees());
                ViewBag.Base64String = EmployeeList;
                foreach (var emp in EmployeeList)
                {
                    PostImages.Add(emp.EmployeeId, Convert.ToBase64String(emp.Photo));

                }
                ViewBag.Images = PostImages;

                return View(EmployeeList);
            }
            else
                return RedirectToAction("Index", "Employee");
        }

        /// <summary>
        /// Search for employees by id,name,department and joining date
        /// </summary>
        /// <param name="txtIdName"></param>
        /// <param name="fromDate"></param>
        /// <param name="EmployeeView"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(string txtIdName, string SearchDate, EmployeeViewModel EmployeeView)
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                DateTime JoinDate = DateTime.Now;
                Dictionary<int, string> PostImages = new Dictionary<int, string>();

                if (SearchDate != string.Empty)
                {
                    JoinDate = DateTime.ParseExact(SearchDate, "yyyy-MM-dd", null);
                }

                var SearchList = iEmployeeService.SearchEmployee(txtIdName, JoinDate);
                var EmployeeList = DtoListMappedToView(SearchList);
                ViewBag.Base64String = EmployeeList;

                foreach (var emp in EmployeeList)
                {
                    PostImages.Add(emp.EmployeeId, Convert.ToBase64String(emp.Photo));

                }
                ViewBag.Images = PostImages;

                return View("List", EmployeeList);
            }
            else
                return RedirectToAction("Index", "Employee");
        }


        /// <summary>
        /// Check whether mobile number registered and joining date is valid
        /// </summary>
        /// <param name="MobileNumber"></param>
        /// <param name="JoinDate"></param>
        /// <returns></returns>
        [NonAction]
        public int IsValidDetails(long MobileNumber, DateTime JoinDate)
        {
            int flag = default(int);
            bool IsValidNo = default(bool);
            IsValidNo = iEmployeeService.IsValidMobileNumber(MobileNumber);

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
