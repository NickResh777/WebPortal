using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security {
    static class AesKeyIv{
        private static readonly byte[] _Key ={
             87,   23,   98,  176, 
             13,   47,  117,  208,
             72,   87,   66,  137,
            133,  161,  143,   25
        };

        private static readonly byte[] _IV ={
              87,   38,  98, 176, 
             128,   69, 57, 208,
             72,   132,  26, 137,
            13,  18, 14,  11
        };


        public static byte[] SecretKey{
            get{
                return _Key;
            }
        }

        public static byte[] IV{
            get{
                return _IV;
            }
        }
    }
}
