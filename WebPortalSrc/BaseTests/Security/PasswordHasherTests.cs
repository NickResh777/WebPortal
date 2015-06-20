using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using WebPortal.BusinessLogic.Services.Security;
using WebPortal.BusinessLogic.ServicesImplementation;


namespace BaseTests.Security
{
    [TestFixture]
    public class PasswordHasherTests{
        private Random _random;
        private Sha256PasswordHasher _passwordHasher;
        private const string SamplePlainPassword = "NickResh777KievAwesome";

        [SetUp]
        public void Init(){
            _passwordHasher = new Sha256PasswordHasher();
            _random = new Random();
        }

        [Test]
        public void hash_should_change_when_salt_added(){
            string randomPassword = GetRandomPassword();
            string hashNoSalt = _passwordHasher.PasswordToHash(randomPassword);
            string hashWithSalt = _passwordHasher.PasswordToHash(randomPassword, GetRandomSalt());
            Assert.AreNotEqual(hashNoSalt, hashWithSalt);
        }

        [Test]
        public void hasher_should_throw_ArgumentException_when_NULL_or_empty_password_passed_no_salt(){

            Assert.Throws<ArgumentException>(() => _passwordHasher.PasswordToHash(""));
            Assert.Throws<ArgumentException>(() => _passwordHasher.PasswordToHash(string.Empty));
            Assert.Throws<ArgumentException>(() => _passwordHasher.PasswordToHash(null));
        }

        [Test]
        public void hasher_should_throw_ArgumentException_when_NULL_or_empty_password_passed_has_salt(){
            Assert.Throws<ArgumentException>(() => _passwordHasher.PasswordToHash(""), GetRandomSalt());
            Assert.Throws<ArgumentException>(() => _passwordHasher.PasswordToHash(string.Empty, GetRandomSalt()));
            Assert.Throws<ArgumentException>(() => _passwordHasher.PasswordToHash(null, GetRandomSalt()));
        }

        [Test]
        public void hasher_should_return_a_non_empty_hash_no_salt(){
            string hash = _passwordHasher.PasswordToHash(GetRandomPassword());
            Assert.IsNotNullOrEmpty(hash);
        }

        [Test]
        public void hasher_should_return_a_64_symbols_hash_no_salt(){
            string hash = _passwordHasher.PasswordToHash(GetRandomPassword());
            Assert.IsNotNullOrEmpty(hash);
            Assert.True(hash.Length == 64);
        }

        [Test]
        public void hasher_should_return_a_non_empty_hash_has_salt(){
            string hash = _passwordHasher.PasswordToHash(GetRandomPassword(), GetRandomSalt());
            Assert.IsNotNullOrEmpty(hash);
        }

        [Test]
        public void hasher_should_return_a_64_symbols_hash_has_salt(){
            string hash = _passwordHasher.PasswordToHash(GetRandomPassword(), GetRandomSalt());
            Assert.IsNotNullOrEmpty(hash);
            Assert.True(hash.Length == 64);
        }

        private string GetRandomPassword(){
            var sb = new StringBuilder();
            for (var i = 1; i <= 10; i++){
                sb.Append((char)_random.Next(65, 92));
            }

            return sb.ToString();
        }

        private string GetRandomSalt(){
            return Guid.NewGuid().ToString();
        }
    }
}
