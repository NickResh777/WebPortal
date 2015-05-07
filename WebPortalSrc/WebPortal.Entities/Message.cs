using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities {
    [Table("Messages")]
    public class Message: BaseBusinessEntityWithId {

        public Message(){

            // the message is NOT read from beginning
            IsRead = false;
        }

        public int SenderId{
            get; 
            set; 
        }

        public int ReceiverId{
            get; 
            set; 
        }

        public bool IsRead{
            get; 
            set; 
        }

        public string Header{
            get; 
            set; 
        }

        public string Body{
            get; 
            set; 
        }


        /// <summary>
        /// The Sender of this message
        /// </summary>
        public virtual Members.Member Author{
            get; 
            set; 
        }

        /// <summary>
        /// The Receiver of this message
        /// </summary>
        public virtual Members.Member Receiver{
            get; 
            set; 
        }
    }
}
