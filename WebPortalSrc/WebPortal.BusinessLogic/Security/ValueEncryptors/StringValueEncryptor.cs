using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Security.ValueEncryptors
{
    public class StringValueEncryptor : BaseCryptoObject<string>{
    

        protected override string StringToValue(string encoded){
            return encoded;
        }

        protected override string ValueToString(string value){
            return value;
        }
    }
}
