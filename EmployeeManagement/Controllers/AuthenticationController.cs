using BusinessLayer.Services;
using BusinessLayer.DataTransferObject;
using System.Web.Mvc;
using EmployeeManagement.ViewModel;
using System.Web.Security;


namespace EmployeeManagement.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private ILoginService iLoginService;

        public AuthenticationController()
        {

        }


        public AuthenticationController(ILoginService _iLoginService)
        {
            iLoginService = _iLoginService;
        }

        /// <summary>
        /// Render Login Page
        /// </summary>
        /// <returns></returns>
        // GET: Default
        public ActionResult Login()
        {
            return View("Index");
        }

        /// <summary>
        /// Permit login to authenticated users
        /// </summary>
        /// <param name="LoginView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoLogin(LoginViewModel LoginView)
        {
            bool IsAdmin = default(bool);
            int UserStatus = default(int);

            UserStatus = iLoginService.GetUserStatus(new LoginDto { Username = LoginView.Username, Password = LoginView.Password });

            if (UserStatus == 0)
            {
                IsAdmin = true;
                Session["IsAdmin"] = IsAdmin;
                Session["username"] = LoginView.Username;
                FormsAuthentication.SetAuthCookie(LoginView.Username.ToString(), false);

                return RedirectToAction("Index", "Admin");
            }
            else if (UserStatus == 1)
            {
                IsAdmin = false;
                Session["IsAdmin"] = IsAdmin;
                Session["username"] = LoginView.Username;
                FormsAuthentication.SetAuthCookie(LoginView.Username.ToString(), false);

                return RedirectToAction("Index", "Employee");

            }

            else
            {
                ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return View("Index");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}