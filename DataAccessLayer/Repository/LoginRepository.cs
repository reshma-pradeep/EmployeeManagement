using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using System.Linq;

namespace DataAccessLayer.Repository
{
    public class LoginRepository : ILoginRepository
    {
        Context db = new Context();

        /// <summary>
        /// Check whether user is validated to login
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public int GetUserStatus(Login Login)
        {
            var AdminUsers = db.Logins.Where(s => s.Username == Login.Username).Where(s => s.Password == Login.Password).Where(s=>s.Role=="Admin").FirstOrDefault();

            if (AdminUsers != null)
                return 0;
            else
            {
                var Users = db.Logins.Where(s => s.Username == Login.Username).Where(s => s.Password == Login.Password).Where(s => s.Role == "Employee").FirstOrDefault();
                if (Users != null)
                    return 1;
                else
                    return 2;
            }
        }

        /// <summary>
        /// Retrieve the role of authenticated user
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public string GetRole(string Username)
        {
            string Role = string.Empty;
            Role = db.Logins.Where(s => s.Username == Username).Select(s => s.Role).FirstOrDefault();
            return Role;
        }


    }
}
