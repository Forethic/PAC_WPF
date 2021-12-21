using System.Threading.Tasks;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Succeess,
        PasswordDoNotMatch,
        EmailAlreadExists,
        UsernameAlreadExists,
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string passward, string confirmPassword);

        Task<Account> Login(string username, string password);
    }
}