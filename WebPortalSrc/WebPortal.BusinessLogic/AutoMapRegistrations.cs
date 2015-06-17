using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using WebPortal.BusinessLogic.AutoMapConverters;
using WebPortal.BusinessLogic.DTOs;
using WebPortal.Entities.Members;

namespace WebPortal.BusinessLogic
{
    public static class AutoMapRegistrations
    {
        public static void Run(){
            Mapper.CreateMap<Member, MemberDto>().
                ConvertUsing<MemberMemberDtoConverter>();
              
        }
    }
}
