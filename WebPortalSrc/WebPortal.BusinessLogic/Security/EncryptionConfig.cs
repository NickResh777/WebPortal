using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    public class EncryptionConfig{
        private readonly byte[] _SecretKey;
        private readonly byte[] _IV;

        public EncryptionConfig(string secretKey, string IV){
            
        }

        public byte[] SecretKey{
            get{
                return _SecretKey;
            }
        }

        public byte[] IV{
            get{
                return _IV;
            }
        }
    }
}
