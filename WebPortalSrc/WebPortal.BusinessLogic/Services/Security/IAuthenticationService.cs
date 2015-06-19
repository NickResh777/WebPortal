using WebPortal.BusinessLogic.ServicesImplementation.Auth;

namespace WebPortal.BusinessLogic.Services.Security {
    public interface IAuthenticationService{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <param name="loginProvider"></param>
        /// <returns></returns>
        LogInResult LogIn(string nickName, string password, ILogInProvider loginProvider);

        // todo: finish this
       // LogInResult LogIn(string name);

        void LogOut();
    }
}
