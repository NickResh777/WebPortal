using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Text;
using WebPortal.BusinessLogic.Services.Security;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    public class Sha256PasswordHasher : IPasswordHasher{
        private const string SaltedPasswordFormat = "{0}_{1}";

        public string PasswordToHash(string plainPassword, string passwordSalt = null) {
            if (string.IsNullOrEmpty(plainPassword)){   
                  // cannot hash an empty string
                  throw new ArgumentException("<plainPassword> parameter is empty");
            }

            using (SHA256 hash = SHA256.Create()){
                string joinedPass = !string.IsNullOrEmpty(passwordSalt)
                    ? string.Format(SaltedPasswordFormat, passwordSalt, plainPassword)
                    : plainPassword;
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
