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
        private readonly PredicateExpressionBuilder<Member> _predicateBuilder = new PredicateExpressionBuilder<Member>(); 
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

        private void BuildPredicateExpression(SearchMembersQuery searchQuery){
                if (searchQuery.CountryId.HasValue)
                {
                    // append country
                    _predicateBuilder.And(member => (member.Profile.CountryId == searchQuery.CountryId.Value));
                }

                if (searchQuery.RegionStateId.HasValue)
                {
                    // append region/state
                    _predicateBuilder.And(member => member.Profile.RegionStateId == searchQuery.RegionStateId.Value);
                }

                if (searchQuery.CityId.HasValue)
                {
                    // append city
                    _predicateBuilder.And(member => member.Profile.CityId == searchQuery.CityId.Value);
                }

                if (searchQuery.Gender != Gender.NotDefined)
                {
                    // add gender filter
                    string genderValue = (searchQuery.Gender == Gender.Male) ? "M" : "F";
                    _predicateBuilder.And(member => member.Gender == genderValue);
                }

            if (searchQuery.MaximalAge.HasValue){
                // todo: FINISH this! 
                _predicateBuilder.And(m => m.Profile.DateOfBirth.HasValue);
            }
        } 

        public IList<MemberDto> Search(SearchMembersQuery searchQuery){
            _predicateBuilder.Reset();
            // initialize predicates
            BuildPredicateExpression(searchQuery);
            var result = _membersRepository.GetWhere(_predicateBuilder.PredicateResult, m => m.Profile);
            return null;
        }

        private bool OnlineMembersRequested(SearchMembersQuery searchQuery){
            // Property [IsOnline] defined and equals TRUE 
            return (searchQuery.IsOnline.HasValue && searchQuery.IsOnline.Value);
        }
    }
}
