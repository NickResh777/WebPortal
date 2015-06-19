using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Services.Security {
    /// <summary>
    /// Interface to denote the login provider 
    /// </summary>
    public interface ILogInProvider{

        /// <summary>
        /// Name of the provider
        /// </summary>
        string Name { get; }



        ///
        bool Authenticate(string userId, string password);
    }
}
