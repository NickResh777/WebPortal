using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{

    public interface IValueEncryptor{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Encrypt(object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        object Decrypt(string encryptedValue);
    }



    public interface IValueEncryptor<T> : IValueEncryptor {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string Encrypt(T value);

        new T Decrypt(string encryptedText);
    }
}
