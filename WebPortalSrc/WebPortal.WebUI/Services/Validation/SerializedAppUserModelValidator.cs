using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPortal.BusinessLogic.Security;
using WebPortal.WebUI.Models;

namespace WebPortal.WebUI.Services.Validation
{
    public class SerializedAppUserModelValidator{
        private static readonly string[] AppUserRoles ={
            WebPortalUserRoles.RoleMember,  // member
            WebPortalUserRoles.RoleAdmin    // admin
        };

        private SerializedAppUserModelDecryptor _modelDecryptor;

        public SerializedAppUserModelValidator(SerializedAppUserModelDecryptor modelDecryptor){
            _modelDecryptor = modelDecryptor;
        }

        public SerializedAppUserModelValidator(SerializedAppUserModel appUserModel, 
                                               IEncryptionProvider encryptionProvider){
            _modelDecryptor = new SerializedAppUserModelDecryptor(encryptionProvider);
            _modelDecryptor.AppUserModel = appUserModel;
        }

        public SerializedAppUserModelDecryptor ModelDecryptor{
            get{
                return _modelDecryptor;
            }
            set{
                _modelDecryptor = value;
            }
        }

        public bool IsValidModel{
            get{
                return IsValidRole && IsValidTrialMembership;
            }
        }

        public bool IsValidRole{
            get{
                return !string.IsNullOrEmpty(ModelDecryptor.DecryptedRole) &&
                       AppUserRoles.Contains(ModelDecryptor.DecryptedRole);
            }
        }

        public bool IsValidTrialMembership{
            get{
                string value = ModelDecryptor.DecryptedIsTrialMembership;
                bool boolValue;
                return !string.IsNullOrEmpty(value) && Boolean.TryParse(value, out boolValue);
            }
        }
    }
}