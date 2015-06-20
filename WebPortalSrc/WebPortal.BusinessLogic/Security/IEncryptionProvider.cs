using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    /// <summary>
    /// Abstract interface to define set of functions 
    /// to deal with encryption
    /// </summary>
    public interface IEncryptionProvider{
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValueEncryptor<T> GetEncryptor<T>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Encrypt(object value);
    }
}
