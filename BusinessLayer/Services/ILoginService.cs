using BusinessLayer.DataTransferObject;

namespace BusinessLayer.Services
{
    public interface ILoginService
    {
        int GetUserStatus(LoginDto LoginDto);
        string GetRole(string Username);
        bool IsAccountLocked(string Username);
        int GetAttemptCount(string Username);
        void NewValidAttempt(string Username);
    }
}
