using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPortal.BusinessLogic.Services.Security {
    class HashingAlgorithmsFactory {

        public HashAlgorithm Create(HashingAlgorithm algorithm){
            if (algorithm == HashingAlgorithm.MD5){
                return MD5.Create();
            } else if (algorithm == HashingAlgorithm.SHA256){
                return new SHA256CryptoServiceProvider();
            }

            return null;
        }
    }
}
