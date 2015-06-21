using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using DatingHeaven.Core;
using Newtonsoft.Json;
using NLog;
using WebPortal.BusinessLogic.Security;
using WebPortal.BusinessLogic.Security.Identities;
using WebPortal.WebUI.Models;
using WebPortal.WebUI.Services.Validation;
using System.Threading;

namespace WebPortal.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication{
        protected Logger Logger;

        protected void Application_Start(){
            Logger = LogManager.GetCurrentClassLogger();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            InjectionContainerConfig.Register();
        }


        public void Application_Authenticate(object sender, EventArgs e){
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null){
                // Member or Admin
                try{
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    SerializedAppUserModel appUserModel =
                        (SerializedAppUserModel) JsonConvert.DeserializeObject(authTicket.UserData);
                    SerializedAppUserModelValidator validator = CreateValidator(appUserModel);

                    if (validator.IsValidModel){
                        // checking encrypted model properties
                        ImpersonateFromModel(appUserModel, validator.ModelDecryptor);
                    } else{
                        // auth ticket was changed or corrupted
                        SetGuestPrincipal();
                    }
                } catch(Exception ex){
                    Logger.Error(ex, "Failed to get");
                    SetGuestPrincipal();
                }

            } else{
                 // guest visitor
                 // Authentication cookie was not found, so this request is
                 // being committed by guest
                 SetGuestPrincipal(); 
            }
        }

        protected void ImpersonateFromModel(SerializedAppUserModel appUserModel, SerializedAppUserModelDecryptor modelDecryptor){
            bool isAdministratorRole = WebPortalUserRoles.IsAdministratorRole(modelDecryptor.DecryptedRole);
            bool isMemberRole = WebPortalUserRoles.IsMemberRole(modelDecryptor.DecryptedRole);

            if (isAdministratorRole){
                 AdminIdentity adminIdentity = new AdminIdentity(appUserModel.AppUserId);
                 WebPortalPrincipal adminPrincipal = new WebPortalPrincipal(
                    identity: adminIdentity,
                    role: WebPortalUserRoles.RoleAdmin
                );
                SetPrincipal(adminPrincipal);
            } else if (isMemberRole){
               //  MemberIdentity memberIdentity = new MemberIdentity();
               //  WebPortalPrincipal memberPrincipal = new WebPortalPrincipal( memberIdentity, WebPortalUserRoles.RoleMember)
               //  SetPrincipal(memberIdentity);
            } 
        }

        protected void SetGuestPrincipal(){
            // not authentication cookie detected, so the user is just a guest
            // not signed in
            GuestIdentity guestIdentity = new GuestIdentity();
            WebPortalPrincipal guestPrincipal = new WebPortalPrincipal(
                   role: BusinessLogic.Security.WebPortalUserRoles.RoleGuest,
                   identity: guestIdentity);
            SetPrincipal(guestPrincipal);
        }

        protected void SetPrincipal(WebPortalPrincipal webPortalPrincipal){
            Context.User = webPortalPrincipal;
            Thread.CurrentPrincipal = webPortalPrincipal;
        }

        private SerializedAppUserModelValidator CreateValidator(SerializedAppUserModel model){
            var modelDecryptor = GlobalInjectionContainer.Instance.Get<SerializedAppUserModelDecryptor>();
            modelDecryptor.AppUserModel = model;
            return new SerializedAppUserModelValidator(modelDecryptor);
        }
    }
}