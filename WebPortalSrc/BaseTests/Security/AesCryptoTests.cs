using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Security.Cryptography;

namespace BaseTests.Security
{
    [TestFixture]
    public class AesCryptoTests{
        private Aes _aesAlgorithm;
        private const string ConstMessage = "The lazy bitch was fucking around and sucked cocksfffffflkjlkdflkkldf";

        private readonly byte[] secretKey128 ={
            77,   99,   12,  122,
            83,  198,  224,  109,
            22,   72,  203,  143,
            77,   154,  12,  177
        };

        private readonly byte[] IV ={
            34, 33, 99, 23,
            88, 32, 12, 33,
            98, 33, 12, 99,
            11, 22, 33, 44
        };



        [SetUp]
        public void Init(){
           _aesAlgorithm = new AesManaged();
           _aesAlgorithm.Key = secretKey128;
            _aesAlgorithm.IV = IV;
            Debug.WriteLine("Key size in bits: " + _aesAlgorithm.KeySize);
        }

        [Test]
        public void EncryptMessage(){
            byte[] data = Encoding.UTF8.GetBytes(ConstMessage);

            ICryptoTransform encryptor = _aesAlgorithm.CreateEncryptor();
            byte[] encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);

        }

        [Test]
        public void EncryptDecryptMessage(){
            byte[] data = Encoding.UTF8.GetBytes("mother_bitch");

            ICryptoTransform encryptor = _aesAlgorithm.CreateEncryptor();
            byte[] encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
            encryptor.Dispose();

            ICryptoTransform decryptor = _aesAlgorithm.CreateDecryptor();
            byte[] decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            decryptor.Dispose();

            string decryptedText = Encoding.UTF8.GetString(decryptedData);
            Debug.WriteLine("DECRYPTED TEXT: " + decryptedText);
        }


        [Test]
        public void show_available_key_sizes(){
            KeySizes[] size = _aesAlgorithm.LegalKeySizes;
            foreach (KeySizes keySize in size){
                  Debug.WriteLine("Min size: " + keySize.MinSize);
                  Debug.WriteLine("Max size: " + keySize.MaxSize);
                  Debug.WriteLine("====================");
            }
        }

        public void dd(){
            
        }

        [TearDown]
        public void Shutdown(){
            if (_aesAlgorithm != null){
                _aesAlgorithm.Dispose();
            }
        }

    }
}
