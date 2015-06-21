using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    /// <summary>
    /// Abstract interface to define set of functions 
    /// to deal with encryption of types
    /// </summary>
    public interface IEncryptionProvider{        
        /// <summary>
        /// 
        /// </summary>
        string Encrypt<T>(T value);

        /// <summary>
        /// 
        /// </summary>
        T Decrypt<T>(string encryptedText);

        /// <summary>
        /// 
        /// </summary>
        string EncryptText(string plainText);

        /// <summary>
        /// 
        /// </summary>
        string DecryptText(string encryptedText);
    }
}
