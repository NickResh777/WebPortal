using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    public static class WebPortalUserRoles{
        public const string RoleGuest = "guest";
        public const string RoleMember = "member";
        public const string RoleAdmin = "admin";

        public const int RoleMemberId = 1;
        public const int RoleAdminId = 2;

        public static string GetRoleName(int roleId){
            if (roleId == RoleAdminId)
                return RoleAdmin;
            if (roleId == RoleMemberId)
                return RoleMember;

            return null;
        }

        public static bool IsAdministratorRole(string role){
            return RoleAdmin.Equals(role);
        }

        public static bool IsMemberRole(string role){
            return RoleMember.Equals(role);
        }
    }
}
