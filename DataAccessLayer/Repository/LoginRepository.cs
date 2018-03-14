using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using System.Linq;
using System;
using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    public class LoginRepository : ILoginRepository
    {
        
        /// <summary>
        /// Check whether user is validated to login
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public int GetUserStatus(Login Login)
        {
            using (var db = new Context())
            {
                var AdminUsers = db.Logins.Where(s => s.Username == Login.Username).Where(s => s.Password == Login.Password).Where(s => s.Role == "Admin").FirstOrDefault();

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
        }

        /// <summary>
        /// Retrieve the role of authenticated user
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public string GetRole(string Username)
        {
            string Role = string.Empty;
            using (var db = new Context())
            {
                Role = db.Logins.Where(s => s.Username == Username).Select(s => s.Role).FirstOrDefault();
            }
            return Role;
        }

        /// <summary>
        /// Returns the current no of invalid attempts
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>Attempts</returns>
        public int GetAttemptCount(string Username)
        {
            int AttemptCount = default(int);
            using (var db = new Context())
            {
                var Attempts = db.Employees.Where(s => s.EmployeeCode == Username).Select(s => s.Attempts).FirstOrDefault();
                AttemptCount = Convert.ToInt32(Attempts);
                return AttemptCount;
            }
        }

        /// <summary>
        /// Lock the account if maximum no of invalid attempts completed
        /// </summary>
        /// <param name="Username"></param>
        public void LockAccount(string Username)
        {
            using (var db = new Context())
            {
                var Employees = db.Employees.Where(s => s.EmployeeCode == Username).FirstOrDefault();
                Employees.IsLocked = true;
                db.Entry(Employees).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Record the invalid attempts for particular user
        /// </summary>
        /// <param name="Username"></param>
        public void NewInvalidAttempt(string Username)
        {
            using (var db = new Context())
            {
                var Employees = db.Employees.Where(s => s.EmployeeCode == Username).FirstOrDefault();
                Employees.Attempts += 1;
                db.Entry(Employees).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Clear invalid attempts on successful login
        /// </summary>
        /// <param name="Username"></param>
        public void NewValidAttempt(string Username)
        {
            using (var db = new Context())
            {
                var Employees = db.Employees.Where(s => s.EmployeeCode == Username).FirstOrDefault();
                Employees.Attempts = 0;
                db.Entry(Employees).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
