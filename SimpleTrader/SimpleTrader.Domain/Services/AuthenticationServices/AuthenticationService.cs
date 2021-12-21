using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _AccountService;
        private readonly IPasswordHasher _PasswordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _AccountService = accountService;
            _PasswordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _AccountService.GetByUsername(username);
            PasswordVerificationResult passwordResult = _PasswordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }


        public async Task<RegistrationResult> Register(string email, string username, string passward, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Succeess;

            if (passward != confirmPassword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }

            Account emailAccount = await _AccountService.GetByEmail(email);
            if (emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadExists;
            }

            Account usernameAccount = await _AccountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadExists;
            }

            if (result == RegistrationResult.Succeess)
            {
                string hashedPassword = _PasswordHasher.HashPassword(passward);

                User user = new User()
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DatedJoined = DateTime.Now,
                };

                Account account = new Account()
                {
                    AccountHolder = user,
                };

                await _AccountService.Create(account);
            }

            return result;
        }
    }
}