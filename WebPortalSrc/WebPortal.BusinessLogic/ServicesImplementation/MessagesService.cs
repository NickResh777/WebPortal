using System;
using System.Collections.Generic;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer;
using WebPortal.Entities;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class MessageService : BaseService, IMessageService{
        private readonly IRepository<Message> _messagesRepo;

        public MessageService(IRepository<Message> messagesRepository,
                               IEntityContextProvider entityContextProvider): 
            base(entityContextProvider){
                   _messagesRepo = messagesRepository;
        }  


        public Entities.Message GetMessageById(int messageId){
            return _messagesRepo.GetById(messageId);
        }

        public IList<Entities.Message> GetUnreadMessages(int userId){
            return null;
        }

        public IList<Entities.Message> GetMessages(int userId) {
            throw new NotImplementedException();
        }

        public void DeleteMessage(int userId, int messageId) {
            throw new NotImplementedException();
        }

        public void SendMessage(Entities.Message message) {
            throw new NotImplementedException();
        }

        public void SetMessageAsRead(int userId, int messageId) {
            if (EntityOperations != null){
                  EntityOperations.SetPropertyValue<Message>(messageId, "IsRead", true);
            }
        }
    }
}
