using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using WebPortal.Entities.Members;
using WebPortal.BusinessLogic.DTOs;

namespace WebPortal.BusinessLogic.AutoMapConverters
{
    class MemberMemberDtoConverter : ITypeConverter<Member, MemberDto>{

        public MemberDto Convert(ResolutionContext context){

            Member currentMember = (Member) context.SourceValue;

            var dto = new MemberDto{
                  Id = currentMember.Id, 
                  IsTrial = currentMember.IsTrial
            };

            if (currentMember.Profile != null){
                if (currentMember.Profile.DateOfBirth.HasValue){
                        DateTime dtStart = currentMember.Profile.DateOfBirth.Value;
                        DateTime dtEnd = DateTime.Now;
                        dto.Age = FindDifferenceInYears(dtStart, dtEnd);
                }
            } 

          
            return dto;
        }

        private int FindDifferenceInYears(DateTime dtStart, DateTime dtEnd){
            if (dtStart > dtEnd){
                   throw new ArgumentException("invalid");
            }

            TimeSpan spanYears = dtEnd.Subtract(dtStart);
            return (spanYears.Days/365);
        }
    }
}
