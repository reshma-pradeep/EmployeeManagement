using System;
using BusinessLayer.DataTransferObject;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services
{
    public class LoginService : ILoginService
    {
        private ILoginRepository iLoginRepository;
        public LoginService()
        {

        }

        public LoginService(ILoginRepository _ILoginRepository)
        {
            iLoginRepository = _ILoginRepository;
        }

        /// <summary>
        /// Check whether user is authenticated to login
        /// </summary>
        /// <param name="LoginDto"></param>
        /// <returns></returns>
        public int GetUserStatus(LoginDto LoginDto)
        {
            int UserStatus = default(int);
            UserStatus = iLoginRepository.GetUserStatus(new Login { Username = LoginDto.Username, Password = LoginDto.Password });
            return UserStatus;
        }

        /// <summary>
        /// Retrieve the role of the authenticated user
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public string GetRole(string Username)
        {
            string Role = string.Empty;
            Role = iLoginRepository.GetRole(Username);
            return Role;
        }

        /// <summary>
        /// Check if user account is locked, if not locked update the invalid attempts
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool IsAccountLocked(string Username)
        {
            int LoginAttemptCount = default(int);

            //Check for current no of invalid attempts
            LoginAttemptCount = iLoginRepository.GetAttemptCount(Username);

            //Check If maximum no of attempts not completed
            if (LoginAttemptCount != 3)
            {
                //If already 2 invalid attempts invalid lock the account
                if (LoginAttemptCount == 2)
                    iLoginRepository.LockAccount(Username);
                iLoginRepository.NewInvalidAttempt(Username);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Validate new attempt for each successful authentication
        /// </summary>
        /// <param name="Username"></param>
        public void NewValidAttempt(string Username)
        {
            iLoginRepository.NewValidAttempt(Username);
        }

        /// <summary>
        /// Retrieve the no of invalid attempts done by the user
        /// </summary>
        /// <param name="Username"></param>
        public int GetAttemptCount(string Username)
        {
            int LoginAttemptCount = default(int);

            LoginAttemptCount = iLoginRepository.GetAttemptCount(Username);
            return LoginAttemptCount;
        }
    }
}
