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
    }
}
