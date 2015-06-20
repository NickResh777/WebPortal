using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.BusinessLogic.Security;

namespace WebPortal.BusinessLogic.Services.Security.Providers
{
    class EncryptionProvider : IEncryptionProvider
    {
       

        public IValueEncryptor<T> GetEncryptor<T>(){
            
            return null;
        }



        public string Encrypt(object value){
            Type valueType = value.GetType();
            IValueEncryptor encryptor = GetEncryptorByType(valueType);
            if (encryptor != null){
                // if the appropriate encryptor was found
                encryptor.Encrypt(value);
            }

            return null;
        }

        private IValueEncryptor GetEncryptorByType(Type valueType)
        {
            throw new NotImplementedException();
        }

     
    }
}
