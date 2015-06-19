using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.BusinessLogic.Services;
using WebPortal.BusinessLogic.Services.Security;
using WebPortal.BusinessLogic.ServicesImplementation.Auth;
using WebPortal.DataAccessLayer;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class AuthenticationService: IAuthenticationService{
        private readonly IRepository<AppUser> _repoUsers;
        private readonly IPasswordHasher      _passwordHasher;

        public AuthenticationService(IRepository<AppUser> usersRepository,
                                     IPasswordHasher passwordHasher){
            _repoUsers = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public LogInResult LogIn(string nickName, string password, ILogInProvider logInProvider){
            var result = new LogInResult();

            string passwordHash = _passwordHasher.ToHash(password, HashingAlgorithm.SHA256);
            AppUser appUser = _repoUsers.
                GetWhere(user =>
                    (user.Name == nickName) && (user.PasswordHash == passwordHash)).
                FirstOrDefault();

            if (appUser == null){
                result.IsSuccess = false;
            }

            return result;
        }

        public void LogOut() {
            throw new NotImplementedException();
        }
    }
}
