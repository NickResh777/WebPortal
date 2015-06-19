using System;
using System.Collections.Generic;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer;
using WebPortal.Entities;

namespace WebPortal.BusinessLogic.ServicesImplementation {
    class MessageService :  IMessageService{
        private readonly IRepository<Message> _repoMessages;

        public MessageService(IRepository<Message> messagesRepository){

       
            _repoMessages = messagesRepository;
        }  


        public Message GetMessageById(int messageId){
            return _repoMessages.GetById(messageId);
        }

        public IList<Message> GetUnreadMessages(int userId){
            return null;
        }

        public IList<Message> GetMessages(int userId) {
            throw new NotImplementedException();
        }

        public void DeleteMessage(int userId, int messageId) {
            throw new NotImplementedException();
        }

        public void SendMessage(Message message) {
            throw new NotImplementedException();
        }

        public void SetMessageAsRead(int userId, int messageId) {
           
        }
    }
}
