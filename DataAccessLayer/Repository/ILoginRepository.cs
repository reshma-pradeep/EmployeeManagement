using DataAccessLayer.Models;

namespace DataAccessLayer.Repository
{
    public interface ILoginRepository
    {
        int GetUserStatus(Login Login);
        string GetRole(string Username);
    }
}
