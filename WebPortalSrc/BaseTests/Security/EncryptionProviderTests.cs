using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WebPortal.BusinessLogic.Services.Security.Providers;

namespace BaseTests.Security
{
    [TestFixture]
    public class EncryptionProviderTests{
        private EncryptionProvider _encryptionProvider;

        [SetUp]
        public void Init(){
              _encryptionProvider = new EncryptionProvider();
        }

       

        [Test]
        public void provider_encrypts_string(){
            //
            // C7Q+CUIgc53ofthGL4tZXw==
            //

            string text = "admin";
            Debug.WriteLine("String length: " + text.Length);
            Debug.WriteLine("Length in bytes: " + text.Length * 2);

            string encryptedText = _encryptionProvider.EncryptText(text);
            Debug.WriteLine("Encrypted base64 text: " + encryptedText);
            Debug.WriteLine("Encrypted base64 text length: " + encryptedText.Length);
            Debug.WriteLine("Encrypted base64 text length in bytes: " + encryptedText.Length * 2);

            byte[] nonBase64TextBytes = Convert.FromBase64String(encryptedText);
            string result = Encoding.Unicode.GetString(nonBase64TextBytes);

            Debug.WriteLine("Result string: " + result );
            Debug.WriteLine("Result string length: " + result.Length);
            Debug.WriteLine("Result string length in BYTES: " + result.Length * 2 );


            Debug.WriteLine("Decrypted text: " + _encryptionProvider.DecryptText(encryptedText));
        }
    }
}
