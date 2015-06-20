using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using NUnit.Framework;
using WebPortal.BusinessLogic.Security.ValueEncryptors;

namespace BaseTests.Security
{
    [TestFixture]
    public class ValueEncryptorsTests{
        private StringValueEncryptor _encryptor;
        private BooleanValueEncryptor _booleanEncryptor;
        private IntegerValueEncryptor _intEncryptor;
        private FormsAuthenticationTicket _authTicket;

        [SetUp]
        public void Init(){
            _encryptor = new StringValueEncryptor();
            _booleanEncryptor = new BooleanValueEncryptor();
            _intEncryptor = new IntegerValueEncryptor();
        }


        [Test]
        public void test_non_generic_value_encryptor(){
            _encryptor.Encrypt(12);
            _encryptor.Encrypt(true);
            _encryptor.Encrypt("4444");
        }

        [Test]
        public void tt(){
          _authTicket = new FormsAuthenticationTicket(
              version: 1,
              name: "NickResh777",
              issueDate: DateTime.Now,
              expiration: DateTime.Now.AddHours(3),
              isPersistent: true,
              userData: _encryptor.Encrypt("mother fucker ass cunt bitch nigger")
          );


            string encryptedTicket = FormsAuthentication.Encrypt(_authTicket);

            FormsAuthenticationTicket copy = FormsAuthentication.Decrypt(encryptedTicket);
            
            Debug.WriteLine("Name: " + copy.Name);
            Debug.WriteLine("User data: " + copy.UserData);
            Debug.WriteLine("User data decrypted: " + _encryptor.Decrypt(copy.UserData));
        }
    }
}
