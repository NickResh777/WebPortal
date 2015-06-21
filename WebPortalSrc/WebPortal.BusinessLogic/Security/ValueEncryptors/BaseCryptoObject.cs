using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Security.ValueEncryptors
{
    public abstract class BaseCryptoObject<T> : ICryptoObject<T>{
        private readonly AesManaged _aesAlgorithm;
        

        protected BaseCryptoObject(){
            _aesAlgorithm = new AesManaged();
            _aesAlgorithm.Key = AesKeyIv.SecretKey;
            _aesAlgorithm.IV = AesKeyIv.IV;

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            _aesAlgorithm.Mode = CipherMode.CBC;
        }


        public string Encrypt(T value){
       
            string valueAsString = ValueToString(value);        
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(valueAsString);

            ICryptoTransform encryptor = _aesAlgorithm.CreateEncryptor(
                AesKeyIv.SecretKey,
                AesKeyIv.IV
            );

            using (var memoryStream = new MemoryStream()){
                using (CryptoStream cryptoStream = new CryptoStream( memoryStream, encryptor, CryptoStreamMode.Write)){
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
            ICryptoTransform decryptor = _aesAlgorithm.CreateDecryptor(
                AesKeyIv.SecretKey,
                AesKeyIv.IV
            );

            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes)){
                using (CryptoStream cryptoStream = new CryptoStream (memoryStream, decryptor, CryptoStreamMode.Read)){
                      byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                      int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

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
    }
}
