using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Security.ValueEncryptors
{
    public class IntegerValueEncryptor : BaseValueEncryptor<int>{
      

        protected override int StringToValue(string encoded){
            return Int32.Parse(encoded);
        }

        protected override string ValueToString(int value){
            return value.ToString();
        }
    }
}
