using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Text;
using WebPortal.BusinessLogic.Services.Security;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class Sha256PasswordHasher : IPasswordHasher{
        private const string SaltPasswordFormat = "{0}_{1}";

        public Sha256PasswordHasher(){
            
        }

        public string ToHash(string passwordSalt, string plainPassword) {
            using (SHA256 hash = SHA256.Create()){
                string joinedPass = string.Format(SaltPasswordFormat, passwordSalt, plainPassword);
                byte[] passwordInBytes = Encoding.UTF8.GetBytes(joinedPass);
                byte[] passwordHash = hash.ComputeHash(passwordInBytes);
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
