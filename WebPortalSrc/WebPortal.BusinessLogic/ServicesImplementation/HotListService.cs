using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer;
using WebPortal.Entities;
using WebPortal.Entities.Members;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class HotListService : BaseService, IHotListService{
        private readonly IRepository<HotListEntry> _repoHotListEntries;

        public HotListService(IRepository<HotListEntry> hotListRepository){
            _repoHotListEntries = hotListRepository;
        }


        public bool AddMemberToHotList(int memberId, int targetMemberId, bool notify, string comment) {
            if (memberId == targetMemberId){
                const string exMsg = @"Member and target member have the same IDs. 
                                       Cannot add a member to the HotList of itself. 
                                       ID: {0}";
                var ex = new InvalidOperationException(string.Format(exMsg, memberId));
                throw ex;
            }

            bool alreadyHasInList = CheckIfAlreadyAdded(memberId, targetMemberId);
            if (alreadyHasInList){
                // if we have the 
                return false;
            }

            var newEntry = new HotListEntry{
                    MemberId = memberId,
                    TargetMemberId = targetMemberId,
                    Comment = comment,
                    ShouldNotify = notify
            };


            _repoHotListEntries.Insert(newEntry);

            if (notify){
                    
            }
          
            return true;
        }

        public IList<Member> GetHotMembers(int memberId){
            var membersList = new List<Member>();
            var hotListEntries = _repoHotListEntries.GetWhereInclude(entry => entry.MemberId, memberId,
                entry => entry.TargetMember);

            if (hotListEntries != null){
                // select the TARGET members to the list
                membersList.AddRange(hotListEntries.Select(entry => entry.TargetMember));
            }

            return membersList;
        }

        private bool CheckIfAlreadyAdded(int memberId, int targetMemberId){
            HotListEntry entry = _repoHotListEntries.GetById(
                                        new[]{
                                            memberId, 
                                            targetMemberId
                                        });
            return (entry != null);
        }
    }
}
