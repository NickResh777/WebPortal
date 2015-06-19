using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Services.Security {
    public interface IPasswordHasher{
        /// <summary>
        /// Get hash of the 
        /// </summary>
        /// <param name="passwordSalt"></param>
        /// <param name="plainPassword"></param>
        /// <returns></returns>
        string ToHash(string passwordSalt, string plainPassword);
    }
}
