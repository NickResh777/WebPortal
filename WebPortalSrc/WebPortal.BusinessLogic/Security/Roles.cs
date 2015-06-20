using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    public static class Roles{
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
    }
}
