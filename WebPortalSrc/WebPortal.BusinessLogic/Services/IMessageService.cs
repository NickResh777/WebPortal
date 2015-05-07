using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.BusinessLogic.Services {
    public interface IMessageService{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Message GetMessageById(int messageId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<Message> GetUnreadMessages(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<Message> GetMessages(int userId); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="messageId"></param>
        void DeleteMessage(int userId, int messageId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(Message message);


        /// <summary>
        /// 
        /// </summary>
        void SetMessageAsRead(int userId, int messageId);
    }
}
