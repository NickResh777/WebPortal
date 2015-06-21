using System;
using System.Linq;
using Ninject;
using WebPortal.BusinessLogic.Security;
using WebPortal.WebUI.Models;

namespace WebPortal.WebUI.Services.Validation
{
    public class SerializedAppUserModelDecryptor{
        private          SerializedAppUserModel   _appUserModel;
        private readonly IEncryptionProvider      _encryptionProvider;

        private string _decryptedRole;
        private string _decryptedIsTrialMembership;

        public SerializedAppUserModelDecryptor(IEncryptionProvider encryptionProvider){
            _encryptionProvider = encryptionProvider;
        }

        public SerializedAppUserModel AppUserModel{
            get{
                return _appUserModel;
            }
            set{
               ClearDecryptedValues();
               _appUserModel = value;
            }            
        }

        private void ClearDecryptedValues(){
            _decryptedRole = null;
            _decryptedIsTrialMembership = null;
        }


        public string DecryptedRole{
            get{
                if (_decryptedRole == null){
                      _decryptedRole = _encryptionProvider.DecryptText(AppUserModel.EncryptedRole);
                }

                return _decryptedRole;
            }
        }

        public string DecryptedIsTrialMembership{
            get{
                if (_decryptedIsTrialMembership == null){
                    _decryptedIsTrialMembership =
                        _encryptionProvider.DecryptText(AppUserModel.EncryptedIsTrialMembership);
                }

                return _decryptedIsTrialMembership;
            }
        }
    }
}