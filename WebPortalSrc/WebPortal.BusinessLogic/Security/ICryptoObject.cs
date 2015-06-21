using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    public interface ICryptoObject<T> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string Encrypt(T value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        T Decrypt(string encryptedText);
    }
}
