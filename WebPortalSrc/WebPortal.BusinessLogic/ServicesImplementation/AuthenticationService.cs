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

   
        public void LogOut() {
            throw new NotImplementedException();
        }

        public AuthenticationResult LogInByName(string name, string password){
            AuthenticationResult logInResult = new AuthenticationResult();

            AppUser appUser = _repoUsers.GetWhere(user => user.Name == name, user => user.Member)
                                         .FirstOrDefault();
            if (appUser == null){
                // user with the provided name was not found
                SetResult_UserNotFoundByName(logInResult);
            } else{
                if (ProvidedPasswordIsCorrect(password, appUser)){
                    // update the last logged in property
                    UpdateLastLoggedInProperty(appUser);
                    SetResult_UserLoggedInSuccessfully(logInResult, appUser);
                } else{
                    // password was wrong
                    SetResult_PasswordIsWrong(logInResult);
                }
            }                                    

            return logInResult;
        }

        private void SetResult_UserNotFoundByName(AuthenticationResult logInResult){
            logInResult.IsAuthenticated = false;
            logInResult.FailReason = LogInFailReason.UserByNameNotFound;
            logInResult.AuthenticatedUser = null;
        }

        private void SetResult_UserLoggedInSuccessfully(AuthenticationResult logInResult, AppUser appUser){
            logInResult.IsAuthenticated = true;
            logInResult.FailReason = LogInFailReason.NoError;
            logInResult.AuthenticatedUser = appUser;
        }

        private void SetResult_PasswordIsWrong(AuthenticationResult logInResult){
            logInResult.IsAuthenticated = false;
            logInResult.FailReason = LogInFailReason.InvalidPassword;
            logInResult.AuthenticatedUser = null;
        }

        private bool ProvidedPasswordIsCorrect(string password, AppUser appUser){
            string userPasswordSalt = appUser.PasswordSalt.ToString();
            string resultHash = _passwordHasher.ToHash(userPasswordSalt, password);

            return string.CompareOrdinal(resultHash, appUser.PasswordHash) == 0;
        }

        private void UpdateLastLoggedInProperty(AppUser appUser){
            appUser.LastLoggedInOn = DateTime.Now;
            _repoUsers.Refresh(appUser);
            _repoUsers.SaveSetChanges();
        }

       

       

        public AuthenticationResult AuthenticateUserByEmail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public AuthenticationResult AuthenticateUserByName(string name, string password)
        {
            AuthenticationResult authResult = new AuthenticationResult();

            AppUser appUser = _repoUsers.GetWhere(user => user.Name == name, user => user.Member)
                                         .FirstOrDefault();
            if (appUser == null) {
                // user with the provided name was not found
                SetResult_UserNotFoundByName(authResult);
            } else {
                if (ProvidedPasswordIsCorrect(password, appUser)){
                    // update the last logged in property
                    UpdateLastLoggedInProperty(appUser);
                    SetResult_UserLoggedInSuccessfully(authResult, appUser);
                }
                else
                {
                    // password was wrong
                    SetResult_PasswordIsWrong(authResult);
                }
            }

            return authResult;
        }
    }
}
