using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string NickName{ get; set; }

        public bool IsTrial { get; set; } 

        public int? Age { get; set; }
    }
}
