using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Security.ValueEncryptors
{
    public abstract class BaseValueEncryptor<T> : IValueEncryptor<T>{
        private readonly SymmetricAlgorithm _symmetricAlgorithm;
        

        protected BaseValueEncryptor(){
            _symmetricAlgorithm = new AesManaged();
            _symmetricAlgorithm.Key = AesKeyIv.SecretKey;
            _symmetricAlgorithm.IV = AesKeyIv.IV;

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            _symmetricAlgorithm.Mode = CipherMode.CBC;
        }


        public string Encrypt(T value){
       
            string valueAsString = ValueToString(value);        
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(valueAsString);

            ICryptoTransform encryptor = _symmetricAlgorithm.CreateEncryptor(
                AesKeyIv.SecretKey,
                AesKeyIv.IV
            );

            using (var memoryStream = new MemoryStream()){

                using (CryptoStream cryptoStream = new CryptoStream( memoryStream, encryptor, CryptoStreamMode.Write))
                {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                        cryptoStream.FlushFinalBlock();

                        byte[] cipherTextBytes = memoryStream.ToArray();

                        string cipherText = Convert.ToBase64String(cipherTextBytes);

                        return cipherText;
                };
            }
        }


        public T Decrypt(string encryptedValue){
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedValue);
            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = _symmetricAlgorithm.CreateDecryptor
            (
                AesKeyIv.SecretKey,
                AesKeyIv.IV
            );

            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes)){
                using (CryptoStream cryptoStream = new CryptoStream (memoryStream,
                                                                     decryptor,
                                                                     CryptoStreamMode.Read)){
                      byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                      int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0,plainTextBytes.Length);

                      string plainText = Encoding.Unicode.GetString( plainTextBytes, 0, decryptedByteCount );
                      return StringToValue(plainText);
                }
            }
        }

        /// <summary>
        /// Convert the decrypted string value back to the actual value
        /// </summary>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        protected abstract T StringToValue(string encryptedValue);
        
        /// <summary>
        /// Convert the actual value to a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract string ValueToString(T value);

        public string Encrypt(object value)
        {
            if (TypesMatch(value.GetType(), typeof (T))){
                //
                return Encrypt((T) value);
            }

            return null;
        }

        private bool TypesMatch(Type t1, Type t2){
            return (t1 == t2);
        }

        object IValueEncryptor.Decrypt(string encryptedValue){
            
            return null;
        }
    }
}
