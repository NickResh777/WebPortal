using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebPortal.BusinessLogic.Security;
using WebPortal.BusinessLogic.Security.ValueEncryptors;

namespace WebPortal.BusinessLogic.Services.Security.Providers
{
    public class EncryptionProvider : IEncryptionProvider
    {
        private static readonly Hashtable EncryptorsMap = new Hashtable();

        static EncryptionProvider(){
            var encryptorTypesList = (from type in Assembly.GetAssembly(typeof (BaseCryptoObject<>)).GetTypes()
                                      where type.IsClass && 
                                            !type.IsAbstract && 
                                            (type.BaseType != null) &&
                                            type.GetInterface("IValueEncryptor`1") != null
                                      select type).ToList();

            encryptorTypesList.ForEach(encType =>{
                Type genericValueType = encType.BaseType.GetGenericArguments()[0];
                EncryptorsMap[genericValueType] = encType;
            });
        }             
       

        private ICryptoObject<T> GetEncryptor<T>(){
            // get encryptor by type from the cache
            Type encryptorType = (Type)EncryptorsMap[typeof (T)];
            if (encryptorType == null){       
                  // count not find the needed value encryptor for the type T
                  throw new ArgumentException("Failed to find encryptor for type: " + typeof(T));
            }

            return (ICryptoObject<T>)Activator.CreateInstance(encryptorType);
        }           



        public string Encrypt<T>(T value){
            ICryptoObject<T> encryptor = GetEncryptor<T>();
            return encryptor.Encrypt(value);
        }

        
        public string EncryptText(string plainText){
            ICryptoObject<string> textEncryptor = GetEncryptor<string>();
            return textEncryptor.Encrypt(plainText);
        }


        public T Decrypt<T>(string encryptedText){
            ICryptoObject<T> decryptor = GetEncryptor<T>();
            return decryptor.Decrypt(encryptedText);
        }


        public string DecryptText(string encryptedText){
            ICryptoObject<string> decryptor = GetEncryptor<string>();
            return decryptor.Decrypt(encryptedText);
        }
    }
}
