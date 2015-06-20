using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Security.ValueEncryptors
{
    public class BooleanValueEncryptor : BaseValueEncryptor<bool>{
       

        protected override string ValueToString(bool value){
            return value ? "true" : "false";
        }

        protected override bool StringToValue(string encoded)
        {
            if (encoded == "true")
                return true;
            if (encoded == "false")
                return false;

            throw new ArgumentException("Could not get the value from decoded string: " + encoded);
        }
    }
}
