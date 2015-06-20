using System;
using System.Linq;
using WebPortal.BusinessLogic.Services.Security;
using WebPortal.DataAccessLayer;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.ServicesImplementation.Auth {
    class AuthenticationService: IApplicationUserService{
        private readonly IRepository<AppUser> _repoUsers;
        private readonly IPasswordHasher      _passwordHasher;

        public AuthenticationService(IRepository<AppUser> usersRepository,
                                     IPasswordHasher passwordHasher){
            _repoUsers = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public AuthenticationResult AuthenticateByName(string name, string currentPassword){
            AppUser appUser = _repoUsers.GetWhere(user => user.Name == name, user => user.Member)
                                        .FirstOrDefault();
            if (appUser == null){
                // user with the provided name was not found
                return AuthResult_UserNotFoundByName();
            }

            string currentPasswordHash = GetPasswordHash(appUser.PasswordSalt, currentPassword);

            if (PasswordHashesMatch(currentPasswordHash, appUser.PasswordHash)){
                // OK, successful login      
                return AuthResult_UserLoggedInSuccessfully(appUser);
            } else{
                // password was wrong
                return AuthResult_InvalidPassword();
            }
        }

        public AuthenticationResult AuthenticateByEmail(string email, string currentPassword){
            AppUser appUser = _repoUsers.GetWhere(user => user.Email == email, user => user.Member)
                                        .FirstOrDefault();
            if (appUser == null){
                return AuthResult_UserNotFoundByEmail();
            }

            string currentPasswordHash = GetPasswordHash(appUser.PasswordSalt, currentPassword);
            if (PasswordHashesMatch(currentPasswordHash, appUser.PasswordHash)){

                return AuthResult_UserLoggedInSuccessfully(appUser);
            } else{
                return AuthResult_InvalidPassword();
            }
        }

        private string GetPasswordHash(Guid passwordSalt, string plainPassword){
            return _passwordHasher.PasswordToHash(passwordSalt.ToString(), plainPassword);
        }

        private AuthenticationResult AuthResult_UserNotFoundByEmail(){
            return new AuthenticationResult(isAuthenticated: false,
                                            failReason: AuthFailReason.UserNotFoundByEmal,
                                            appUser: null);
        }

        private AuthenticationResult AuthResult_UserNotFoundByName(){
            return new AuthenticationResult(isAuthenticated: false,
                                            failReason: AuthFailReason.UserNotFoundByName,
                                            appUser: null);
        }

        private AuthenticationResult AuthResult_UserLoggedInSuccessfully( AppUser appUser){
            return new AuthenticationResult(isAuthenticated: true,
                                            failReason: AuthFailReason.NoError,
                                            appUser: appUser);
        }

        private AuthenticationResult AuthResult_InvalidPassword(){
            return new AuthenticationResult(isAuthenticated: false,
                                            failReason: AuthFailReason.InvalidPassword,
                                            appUser: null);
        }

        private bool PasswordHashesMatch(string currentPasswordHash, string userPasswordHash){
            return string.CompareOrdinal(currentPasswordHash, userPasswordHash) == 0;
        }
    }
}
