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
        /// <param name="plainPassword"></param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        string ToHash(string plainPassword, HashingAlgorithm hashAlgorithm);
    }
}
