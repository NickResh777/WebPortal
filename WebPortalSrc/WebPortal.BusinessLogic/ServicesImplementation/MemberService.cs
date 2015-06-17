using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using WebPortal.BusinessLogic.DTOs;
using WebPortal.BusinessLogic.DTOs.Queries;
using WebPortal.BusinessLogic.OnlineUsers;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer;
using WebPortal.Entities.Members;
using System.Linq.Expressions;

namespace WebPortal.BusinessLogic.ServicesImplementation
{
    class MemberService: BaseService, IMemberService{
        private readonly IRepository<Member> _membersRepository;
        private readonly OnlineUsersStorage  _onlineStorage;


        public MemberService(IRepository<Member> membersRepo,
                             OnlineUsersStorage onlineUsersStorage){
            _onlineStorage = onlineUsersStorage;
            _membersRepository = membersRepo;
        }

        public MemberDto GetMemberById(int memberId){
            Member member = _membersRepository.GetById(memberId, m => m.Profile);

            return Mapper.Map<MemberDto>(member);
        }

        public Member GetMemberByEmail(string email){
            Member member = _membersRepository.GetWhere(m => (m.Email == email)).
                                              FirstOrDefault();
            return member;
        }                             

        public Member GetMemberByNickName(string nickname)
        {
            throw new NotImplementedException();
        }

        public Member CreateMember(string nickname, string firstName, string lastName, Entities.Members.Gender gender, DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        private void BuildPredicateExpression(PredicateExpressionBuilder<Member> expBuilder,
            SearchMembersQuery searchQuery){
                if (searchQuery.CountryId.HasValue)
                {
                    // append country
                    expBuilder.And(member => (member.Profile.CountryId == searchQuery.CountryId.Value));
                }

                if (searchQuery.RegionStateId.HasValue)
                {
                    // append region/state
                    expBuilder.And(member => member.Profile.RegionStateId == searchQuery.RegionStateId.Value);
                }

                if (searchQuery.CityId.HasValue)
                {
                    // append city
                    expBuilder.And(member => member.Profile.CityId == searchQuery.CityId.Value);
                }

                if (searchQuery.Gender != Gender.NotDefined)
                {
                    // add gender filter
                    string genderValue = (searchQuery.Gender == Gender.Male) ? "M" : "F";
                    expBuilder.And(member => member.Gender == genderValue);
                }

            if (searchQuery.MaximalAge.HasValue){
                // todo: FINISH this! 
                expBuilder.And(m => m.Profile.DateOfBirth.HasValue);

            }
        } 

        public IList<MemberDto> Search(SearchMembersQuery searchQuery){
            PredicateExpressionBuilder<Member> predicateBuilder = new PredicateExpressionBuilder<Member>();
            // initialize predicates
            BuildPredicateExpression(predicateBuilder, searchQuery);


            var result = _membersRepository.GetWhere(predicateBuilder.GetExpression(), m => m.Profile);
            return null;
        }

        private bool OnlineMembersRequested(SearchMembersQuery searchQuery){
            // Property [IsOnline] defined and equals TRUE 
            return (searchQuery.IsOnline.HasValue && searchQuery.IsOnline.Value);
        }
    }
}
