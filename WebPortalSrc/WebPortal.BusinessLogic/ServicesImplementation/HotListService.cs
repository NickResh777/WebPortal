using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;
using WebPortal.Entities.Members;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class HotListService :  IHotListService{
        private readonly IRepository<HotListEntry> _repoHotListEntries;
        private readonly IMemberNotificationService _memberNotifier;
        

        public HotListService(IRepository<HotListEntry> hotListRepository,
                              IMemberNotificationService notoficationService){
            _repoHotListEntries = hotListRepository;
            _memberNotifier = notoficationService;
        }


        public bool AddMemberToHotList(int memberId, int targetMemberId, bool notify, string comment) {
            if (memberId == targetMemberId){
                const string exMsg = @"Member and target member have the same IDs. 
                                       Cannot add a member to the HotList of itself. 
                                       ID: {0}";
                var ex = new InvalidOperationException(string.Format(exMsg, memberId));
                throw ex;
            }

            try{
                bool alreadyHasInList = CheckIfAlreadyAdded(memberId, targetMemberId);
                if (alreadyHasInList){
                    // if already added
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
                    // notify the member
                    _memberNotifier.NotifyAddedToHotList(memberId, targetMemberId, comment);
                }

                return true;
            } catch (Exception ex){

                throw;
            }
        }

        public IList<Member> GetHotMembers(int memberId){
            try{
                return _repoHotListEntries.GetWhere(hle => (hle.MemberId == memberId), 
                                                   hle => hle.TargetMember)
                                          .Select(hle => hle.TargetMember).ToList();
            } catch (Exception ex){
                throw;
            }
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
