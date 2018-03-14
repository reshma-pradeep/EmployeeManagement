using DataAccessLayer.Models;

namespace DataAccessLayer.Repository
{
    public interface ILoginRepository
    {
        int GetUserStatus(Login Login);
        string GetRole(string Username);

        int GetAttemptCount(string Username);
        void LockAccount(string Username);
        void NewInvalidAttempt(string Username);
        void NewValidAttempt(string Username);
    }
}
