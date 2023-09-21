using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string email, string password, string firstName, string lastName);
        bool VerifyPassword(string inputPassword, string hashedPassword);
        Task<string> AuthenticateAsync(string email, string password);
    }
}
