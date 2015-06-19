using WebPortal.BusinessLogic.ServicesImplementation.Auth;

namespace WebPortal.BusinessLogic.Services.Security {
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationService{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProperty"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        AuthenticationResult AuthenticateUserByEmail(string email, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthenticationResult AuthenticateUserByName(string name, string password);
    }
}
