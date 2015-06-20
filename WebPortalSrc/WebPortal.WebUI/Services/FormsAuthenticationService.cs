using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using WebPortal.BusinessLogic.Security;
using WebPortal.BusinessLogic.Services.Security;
using WebPortal.BusinessLogic.ServicesImplementation.Auth;
using WebPortal.DataAccessLayer;
using WebPortal.Entities.Authentication;
using WebPortal.WebUI.Models;

namespace WebPortal.WebUI.Services
{
    class FormsAuthenticationService  : IFormsAuthenticationService{
        private static          JsonSerializerSettings JsonSettings;
        private static volatile bool                   JSettingInitialized;
        private static readonly object                 JsonSettingsLock = new object();

        private readonly IApplicationUserService _authService;
        private readonly IRepository<AppUser> _repoAppUsers;
        private readonly HttpContextBase _httpContext;
        private readonly IEncryptionProvider _encryptionsProvider;
        private Regex _regexEmail;

        public FormsAuthenticationService(IApplicationUserService authService, 
                                          IRepository<AppUser> repoAppUsers, 
                                          HttpContextBase httpContext, 
                                          IEncryptionProvider encryptorsProvider){
            _authService = authService;
            _repoAppUsers = repoAppUsers;
            _httpContext = httpContext;
            _encryptionsProvider = encryptorsProvider;
        }


        public AppUser LogIn(string nameOrEmail, string password, bool isPersistenCookie){
            AuthenticationResult authResult = IsEmail(nameOrEmail)
                ? _authService.AuthenticateByEmail(nameOrEmail, password)
                : _authService.AuthenticateByName(nameOrEmail, password);

            if (authResult.IsAuthenticated){
                    // is authenticated
                    UpdateLastLoggedInProperty(authResult.AuthenticatedUser);
                    SaveAuthCookie(authResult.AuthenticatedUser, isPersistenCookie);
                    return authResult.AuthenticatedUser;
            }

            return null;
        }


        private SerializedAppUserModel EncryptAppUserModel(AppUser appUser){
            var model = new SerializedAppUserModel();
            model.AppUserId = appUser.Id;

            if ((appUser.Member != null) && (appUser.RefMemberId != null)){
                // set [MemberId] property
                model.MemberId = appUser.RefMemberId.Value;
                model.EncryptedIsTrialMembership = _encryptionsProvider.Encrypt(appUser.Member.IsTrial);
                //model.PaidMembershipExpiresOn = 
            }

            model.Email = appUser.Email;
            model.Name = appUser.Name;

            // encrypt the user role since it's crucial for 
            // allowed commands
            string roleName = BusinessLogic.Security.Roles.GetRoleName(appUser.Role);
            model.EncryptedRole = _encryptionsProvider.Encrypt(roleName);
             
            return model;
        }

        private void SaveAuthCookie(AppUser appUser, bool isPersistentCookie){
            SerializedAppUserModel encryptedAppUserModel = EncryptAppUserModel(appUser);
            string serializedJsonModel = JsonConvert.SerializeObject(
                value: encryptedAppUserModel, 
                settings: GetJsonSerializerSettings());

           FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               version: 1,
               name: appUser.Name,
               issueDate: DateTime.Now,
               expiration: DateTime.Now.AddDays(7.0),
               isPersistent: isPersistentCookie,
               userData:  serializedJsonModel
            );

            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            _httpContext.Response.Cookies.Add(authCookie);
        }


        private JsonSerializerSettings GetJsonSerializerSettings(){
            if (!JSettingInitialized){
                lock (JsonSettingsLock){
                    if (!JSettingInitialized){
                       JsonSettings = new JsonSerializerSettings();
                       JsonSettings.NullValueHandling = NullValueHandling.Include;
                       JSettingInitialized = true;
                    }
                }
            }
            return JsonSettings;
        }

       

        private void UpdateLastLoggedInProperty(AppUser appUser){
            appUser.LastLoggedInOn = DateTime.Now;
            _repoAppUsers.Refresh(appUser);
            _repoAppUsers.SaveSetChanges();
        }

        private bool IsEmail(string userNameOrEmail){
            try{
                new MailAddress(userNameOrEmail);
                return true;
            } catch{
                return false;
            }
        }

        public void LogOut()
        {
            // sign out from the website
            FormsAuthentication.SignOut();
        }


    }
}
