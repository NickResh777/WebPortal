using WebPortal.BusinessLogic.ServicesImplementation.Auth;

namespace WebPortal.BusinessLogic.Services.Security {
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationUserService{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProperty"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        AuthenticationResult AuthenticateByEmail(string email, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthenticationResult AuthenticateByName(string name, string password);
    }
}
