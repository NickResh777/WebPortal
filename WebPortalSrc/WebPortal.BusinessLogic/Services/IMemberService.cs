﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.BusinessLogic.DTOs;
using WebPortal.BusinessLogic.DTOs.Queries;
using WebPortal.BusinessLogic.Security;
using WebPortal.Entities.Members;

namespace WebPortal.BusinessLogic.Services {
    public interface IMemberService{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        MemberDto GetMemberById(int memberId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Member GetMemberByEmail(string email);

        IList<MemberDto> Search(SearchMembersQuery searchQuery);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        Member GetMemberByNickName(string nickname);


        Member CreateMember(string nickname,
                string firstName,
                string lastName,
                Gender gender,
                DateTime dateOfBirth);
    }
}
