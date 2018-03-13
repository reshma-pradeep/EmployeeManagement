using BusinessLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface ILoginService
    {
        int GetUserStatus(LoginDto LoginDto);
        string GetRole(string Username);
    }
}
