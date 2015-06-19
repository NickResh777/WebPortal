using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Text;
using WebPortal.BusinessLogic.Services.Security;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class PasswordHasher : IPasswordHasher{
        private readonly HashingAlgorithmsFactory _factory;

        public PasswordHasher(){
            _factory = new HashingAlgorithmsFactory();
        }

        public string ToHash(string plainPassword, 
                             HashingAlgorithm hashAlgorithm){
            HashAlgorithm hashAlg = _factory.Create(hashAlgorithm);
            if (hashAlg == null){
                 throw new InvalidOperationException("hashAlg == null");
            }

            using (hashAlg){
                byte[] passwordInBytes = Encoding.UTF8.GetBytes(plainPassword);
                byte[] passwordHash = hashAlg.ComputeHash(passwordInBytes);
                return ToHash(passwordHash);
            }
        }

        private string ToHash(byte[] bytes){
            var hashBuilder = new StringBuilder();
            foreach(byte hashByte in bytes){
                hashBuilder.Append(hashByte.ToString("x2"));
            }

            return hashBuilder
                .ToString()
                .ToUpper();
        }
    }
}
